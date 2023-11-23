using DalApi;
using DO;
namespace Dal;
using DalXml;
using System.Xml.Linq;


internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        XElement XMLdependecies =XMLTools. LoadListFromXMLElement("Dependencies.xml");
        int newId = Config.NextDependencyId;//create a new running number
        XElement newDependency=new XElement("Dependency",//create a new xml elememnt
            new XElement("Id", newId), 
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask",item.DependsOnTask));
        XMLdependecies.Add(newDependency);//addes to the list       
        XMLTools.SaveListToXMLElement(XMLdependecies, "Dependencies.xml");//send the the updated list to the xml
        return newId;
    }

    public void Delete(int id)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        XElement? elementToDelete = (XElement)XMLdependecies.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(id));
        
        if (elementToDelete != null)
        {
            elementToDelete.Remove();
            XMLTools.SaveListToXMLElement(XMLdependecies, "Dependencies.xml");//send the the updated list to the xml

        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {id} לא קיים");

        }
    }

    public Dependency? Read(int id)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        XElement? elementToRead = (XElement)XMLdependecies.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(id));
        if (elementToRead != null)
        {
            Dependency? dependency = new Dependency();
            dependency.Id = Convert.ToInt32(elementToRead.Element("Id")!.Value);
            dependency.DependentTask = Convert.ToInt32(elementToRead.Element("DependentTask")!.Value);
            dependency.DependsOnTask = Convert.ToInt32(elementToRead.Element("DependsOnTask")!.Value);
            return dependency;
        }
        else
        {
            return null;
        }
    }
    
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        Dependency? elementToRead = XMLdependecies?.Elements("Dependency")
        .Select(d =>
        {
            Dependency dependency = new Dependency();
            dependency.Id = int.TryParse(d.Element("Id")?.Value, out int id) ? id : 0;
            dependency.DependentTask = int.TryParse(d.Element("DependentTask")?.Value, out int dependentTask) ? dependentTask : 0;
            dependency.DependsOnTask = int.TryParse(d.Element("DependsOnTask")?.Value, out int dependsOnTask) ? dependsOnTask : 0;
            return dependency;
        })
        .FirstOrDefault(dependency => filter(dependency));
        if (elementToRead != null)
        {
            return elementToRead;
        }
        else
        {
            return null;
        }
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("Dependencies.xml");

        if (filter != null)
        {
            List<Dependency> list = XMLdependecies!.Elements("Dependency")
            .Select(d =>
            {
                Dependency dependency = new Dependency();
                dependency.Id = int.TryParse(d.Element("Id")?.Value, out int id) ? id : 0;
                dependency.DependentTask = int.TryParse(d.Element("DependentTask")?.Value, out int dependentTask) ? dependentTask : 0;
                dependency.DependsOnTask = int.TryParse(d.Element("DependsOnTask")?.Value, out int dependsOnTask) ? dependsOnTask : 0;
                return dependency;
            })
            .Where(dependency => filter(dependency)).ToList();

            return list;
        }
        else
        {
            List<Dependency> list = XMLdependecies!.Elements("Dependency")
            .Select(d =>
            {
                Dependency dependency = new Dependency();
                dependency.Id = int.TryParse(d.Element("Id")?.Value, out int id) ? id : 0;
                dependency.DependentTask = int.TryParse(d.Element("DependentTask")?.Value, out int dependentTask) ? dependentTask : 0;
                dependency.DependsOnTask = int.TryParse(d.Element("DependsOnTask")?.Value, out int dependsOnTask) ? dependsOnTask : 0;
                return dependency;
            })
            .ToList();

            return list;
        }

    }

    public void Update(Dependency item)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("Dependencies.xml");
        XElement? elementToUpdate = (XElement)XMLdependecies.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(item.Id));

        if (elementToUpdate != null)
        {
            elementToUpdate.Remove();
            XElement updatedDependency = new("Dependency",//create a new xml elememnt
            new XElement("Id", item.Id),
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask", item.DependsOnTask));
            XMLdependecies.Add(updatedDependency);
            XMLTools.SaveListToXMLElement(XMLdependecies, "Dependencies.xml");//send the the updated list to the xml

        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {item.Id} לא קיים");

        }


    }
}
