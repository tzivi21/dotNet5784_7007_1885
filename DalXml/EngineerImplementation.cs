
using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// create a new engineer entity
    /// </summary>
    /// <param name="item">enginner to add</param>
    /// <returns>the id of the enginner that has been added</returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(Engineer item)
    {

        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers");
        Engineer? engineer = Engineers?.FirstOrDefault(engineer => engineer.Id == item.Id);
        if (engineer != null)
        {
            throw new DalAlreadyExistException($"Task object with ID {item.Id} already  exists");
        }
        Engineers!.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, "Engineers");

        return item.Id;
    }
    /// <summary>
    /// delete an engineer entity
    /// </summary>
    /// <param name="id">the id of the engineer to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers");

        Engineer? toDelete = Engineers.FirstOrDefault(t => t.Id == id);
        if (toDelete != null)
        {
            Engineers.Remove(toDelete);
            XMLTools.SaveListToXMLSerializer(Engineers, "Engineers");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {id} does not exist");
        }
    }
    /// <summary>
    /// reads a certaun engineer 
    /// </summary>
    /// <param name="id">the id of the engineer to read</param>
    /// <returns>the engineer with the id</returns>
    public Engineer? Read(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers");
        return Engineers.FirstOrDefault(t => t.Id == id);
    }
    /// <summary>
    /// reads the first engineer that true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>the first enginner that is true to the condition</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers");
        return Engineers.FirstOrDefault(filter);
    }
    /// <summary>
    /// reads all the list of engineers /all the engineers that are true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>a list of items that are true to the condition</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers");
        return filter != null ? Engineers.Where(filter) : Engineers;
    }
    /// <summary>
    /// updated a specific engineer
    /// </summary>
    /// <param name="item">the item for the update</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineers");

        Engineer? toUpdate = Engineers.FirstOrDefault(t => t.Id == item.Id);
        if (toUpdate != null)
        {
            int index = Engineers.IndexOf(toUpdate);
            Engineers[index] = item;
            XMLTools.SaveListToXMLSerializer(Engineers, "Engineers");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {item.Id} does not exist");
        }
    }
}
