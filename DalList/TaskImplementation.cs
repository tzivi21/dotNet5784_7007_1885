namespace Dal;

using System;
using System.Collections.Generic;
using DalApi;
using DO;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newItem = new Task();
        newItem = item;
        newItem.Id = newId;
        DataSource.Tasks.Add(newItem);
        return newId;
    }

    public void Delete(int id)
    {
        // בודק אם כבר יש ברשימה אדם עם אותו ID
        Task objectToDelete = DataSource.Tasks.FirstOrDefault(obj => obj.Id == id);
        if (objectToDelete != null)
        {
            DataSource.Tasks.Remove(objectToDelete);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Task עם ID {id} לא קיים");

        }
    }

    public Task? Read(int id)
    {
        Task objectToRead = DataSource.Tasks.FirstOrDefault(obj => obj.Id == id);
        if (objectToRead != null)
        {
            return objectToRead;
        }
        else
        {
            return null;
        }
    }

    public List<Task> ReadAll()
    {

        List<Task> newTaskList = new List<Task  >();

        foreach (Task item in DataSource.Tasks)
        {
            Task newItem = new Task();
            newItem = item;
            // Create a new instance of the object
            // Copy the properties or fields from the original item to the new item
            // Example: newItem.Property = item.Property;
            newTaskList.Add(newItem);
        }
        return newTaskList;
    }

    public void Update(Task item)
    {
        Task objectToDelete = DataSource.Tasks.FirstOrDefault(obj => obj.Id == item.Id);

        if (objectToDelete != null)
        {
            DataSource.Tasks.Remove(objectToDelete);
            DataSource.Tasks.Add(item);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Task עם ID {item.Id} לא קיים");
        }
    }
}
