namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;


internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// create a new dependency entity
    /// </summary>
    /// <param name="item">dependency to add</param>
    /// <returns>the id of the dependency that has been added</returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(Dependency item)
    {
        XElement XMLdependecies =XMLTools. LoadListFromXMLElement("depndencies");
        int newId = Config.NextDependencyId;//create a new running number
        XElement newDependency = new XElement("Dependency",//create a new xml elememnt
            new XElement("Id", newId));
        if (item != null && item.DependentTask != null)
        {
            newDependency.Add(new XElement("DependentTask", item.DependentTask));
        }

        if (item != null && item.DependsOnTask != null)
        {
            newDependency.Add(new XElement("DependsOnTask", item.DependsOnTask));
        }
        XMLdependecies.Add(newDependency);//addes to the list       
        XMLTools.SaveListToXMLElement(XMLdependecies, "depndencies");//send the the updated list to the xml
        return newId;
    }
    /// <summary>
    /// delete an dependency entity
    /// </summary>
    /// <param name="id">the id of the dependency to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("depndencies");
        XElement? elementToDelete = (XElement)XMLdependecies.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(id.ToString()));
        
        if (elementToDelete != null)
        {
            elementToDelete.Remove();
            XMLTools.SaveListToXMLElement(XMLdependecies, "depndencies");//send the the updated list to the xml

        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {id} לא קיים");

        }
    }
    /// <summary>
    /// reads a certaun dependency 
    /// </summary>
    /// <param name="id">the id of the dependency to read</param>
    /// <returns>the dependency with the id</returns>
    public Dependency? Read(int id)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("depndencies");
        XElement? elementToRead = (XElement)XMLdependecies.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(id.ToString()));
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
    /// <summary>
    /// reads the first dependency that true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>the first dependency that is true to the condition</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("depndencies");
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
    /// <summary>
    /// reads all the list of dependencies /all the dependencies that are true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the condition</param>
    /// <returns>a list of items that are true to the condition</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("depndencies");

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
    /// /// <summary>
    /// updated a specific dependency
    /// </summary>
    /// <param name="item">the item for the update</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Dependency item)
    {
        XElement XMLdependecies = XMLTools.LoadListFromXMLElement("depndencies");
        XElement? elementToUpdate = (XElement)XMLdependecies.Elements("Dependency").FirstOrDefault(d => d!.Element("Id")!.Value.Equals(item.Id.ToString()));

        if (elementToUpdate != null)
        {
            elementToUpdate.Remove();
            XElement updatedDependency = new("Dependency",//create a new xml elements
            new XElement("Id", item.Id),
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask", item.DependsOnTask));
            XMLdependecies.Add(updatedDependency);
            XMLTools.SaveListToXMLElement(XMLdependecies, "depndencies");//send the the updated list to the xml

        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {item.Id} לא קיים");

        }


    }
}
