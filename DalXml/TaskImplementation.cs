using Dal;
using DalApi;
using DalXml;
using DO;


public class TaskImplementation : ITask
{
    /// <summary>
    /// create a new task entity
    /// </summary>
    /// <param name="item">task to add</param>
    /// <returns>the id of the task that has been added</returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(DO.Task item)
    {

        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");//load the list of utems from file

        int newId = Config.NextTaskId;//set the running id
        item.Id = newId;

        tasks.Add(item);//add to the list
        XMLTools.SaveListToXMLSerializer(tasks, "Tasks.xml");

        return newId;
    }
    /// <summary>
    /// delete an task entity
    /// </summary>
    /// <param name="id">the id of the task to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");

        DO.Task? toDelete = tasks.FirstOrDefault(t => t.Id == id);
        if (toDelete != null)
        {
            tasks.Remove(toDelete);
            XMLTools.SaveListToXMLSerializer(tasks, "Tasks.xml");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {id} does not exist");
        }
    }
    /// <summary>
    /// reads a certaun task 
    /// </summary>
    /// <param name="id">the id of the task to read</param>
    /// <returns>the task with the id</returns>
    public DO.Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");
        return tasks.FirstOrDefault(t => t.Id == id);
    }
    /// <summary>
    /// reads the first task that true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>the first task that is true to the condition</returns>
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");
        return tasks.FirstOrDefault(filter);
    }
    /// <summary>
    /// reads all the list of tasks /all the tasks that are true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>a list of items that are true to the condition</returns>
    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");
        return filter != null ? tasks.Where(filter) : tasks;
    }
    /// /// <summary>
    /// updated a specific task
    /// </summary>
    /// <param name="item">the item for the update</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");

        DO.Task? toUpdate = tasks.FirstOrDefault(t => t.Id == item.Id);
        if (toUpdate != null)
        {
            int index = tasks.IndexOf(toUpdate);
            tasks[index] = item;
            XMLTools.SaveListToXMLSerializer(tasks, "Tasks.xml");
        }
        else
        {
            throw new DalDoesNotExistException($"Task object with ID {item.Id} does not exist");
        }
    }
}