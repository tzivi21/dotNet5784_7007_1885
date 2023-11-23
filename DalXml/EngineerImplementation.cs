
using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {

        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers.xml");
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, "Engineers.xml");

        return item.Id;
    }

    public void Delete(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers.xml");

        Engineer? toDelete = Engineers.FirstOrDefault(t => t.Id == id);
        if (toDelete != null)
        {
            Engineers.Remove(toDelete);
            XMLTools.SaveListToXMLSerializer(Engineers, "Engineers.xml");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {id} does not exist");
        }
    }

    public Engineer? Read(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers.xml");
        return Engineers.FirstOrDefault(t => t.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers.xml");
        return Engineers.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers.xml");
        return filter != null ? Engineers.Where(filter) : Engineers;
    }

    public void Update(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers.xml");

        Engineer? toUpdate = Engineers.FirstOrDefault(t => t.Id == item.Id);
        if (toUpdate != null)
        {
            int index = Engineers.IndexOf(toUpdate);
            Engineers[index] = item;
            XMLTools.SaveListToXMLSerializer(Engineers, "Engineers.xml");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {item.Id} does not exist");
        }
    }
}
