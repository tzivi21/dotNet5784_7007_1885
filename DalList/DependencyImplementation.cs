namespace Dal;

using System.Collections.Generic;
using DalApi;
using DO;
internal class DependencyImplementation : IDependency
{

    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;//create a new running number
        Dependency newItem = new Dependency();
        newItem = item;
        newItem.Id= newId;
        DataSource.Dependencies.Add(newItem);
        return newId;

    }

    public void Delete(int id)
    {
        //checks if the dependency is in the list
        Dependency? objectToDelete = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == id);
        if (objectToDelete != null)
        {
            DataSource.Dependencies.Remove(objectToDelete);
        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {id} לא קיים");

        }
    }

    public Dependency? Read(int id)
    {
        //checks if the dependency is in the list
        Dependency? objectToRead = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == id);
        if (objectToRead != null)
        {
            return objectToRead;
        }
        else
        {
            return null;
        }
    }

    public List<Dependency> ReadAll()
    {
        List<Dependency> newDependencyList = new List<Dependency>();

        foreach (Dependency item in DataSource.Dependencies)
        {
            Dependency newItem = new Dependency();
            newItem.Id = item.Id;
            newItem.DependentTask = item.DependentTask;
            newItem.DependsOnTask = item.DependsOnTask;
            newDependencyList.Add(newItem);
        }
        return newDependencyList;

    }

    public void Update(Dependency item)
    {
        Dependency? objectToDelete = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == item.Id);
        //checks if the object to delete exist in the list
        if (objectToDelete != null)
        {
            DataSource.Dependencies.Remove(objectToDelete);
            DataSource.Dependencies.Add(item);
        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Dependency עם ID {item.Id} לא קיים");
        }
    }
}
