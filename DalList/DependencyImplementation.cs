namespace Dal;

using System.Collections.Generic;
using DalApi;
using DO;
public class DependencyImplementation : IDependency
{

    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency newItem = new Dependency();
        newItem = item;
        newItem.Id= newId;
        DataSource.Dependencies.Add(newItem);
        return newId;

    }

    public void Delete(int id)
    {
        // בודק אם כבר יש ברשימה אדם עם אותו ID
        Dependency objectToDelete = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == id);
        if (objectToDelete != null)
        {
            DataSource.Dependencies.Remove(objectToDelete);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Dependency עם ID {id} לא קיים");

        }
    }

    public Dependency? Read(int id)
    {
        Dependency objectToRead = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == id);
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
            newItem = item;
            // Create a new instance of the object
            // Copy the properties or fields from the original item to the new item
            // Example: newItem.Property = item.Property;
            newDependencyList.Add(newItem);
        }
        return newDependencyList;

    }

    public void Update(Dependency item)
    {
        Dependency objectToDelete = DataSource.Dependencies.FirstOrDefault(obj => obj.Id == item.Id);

        if (objectToDelete != null)
        {
            DataSource.Dependencies.Remove(objectToDelete);
            DataSource.Dependencies.Add(item);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Dependency עם ID {item.Id} לא קיים");
        }
    }
}
