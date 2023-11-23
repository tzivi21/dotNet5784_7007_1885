using Dal;
using DalApi;
using DalXml;
using DO;


public class TaskImplementation : ITask
{

    public int Create(DO.Task item)
    {

        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");//load the list of utems from file

        int newId = Config.NextTaskId;//set the running id
        item.Id = newId;

        tasks.Add(item);//add to the list
        XMLTools.SaveListToXMLSerializer(tasks, "Tasks.xml");

        return newId;
    }

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

    public DO.Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");
        return tasks.FirstOrDefault(t => t.Id == id);
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");
        return tasks.FirstOrDefault(filter);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>("Tasks.xml");
        return filter != null ? tasks.Where(filter) : tasks;
    }

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