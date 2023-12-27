namespace Dal;
using System;
using System.Collections.Generic;
using DalApi;
using DO;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// resets the list of tasks
    /// </summary>
    public void Reset()
    {
        DataSource.Tasks.Clear();
    }
    /// <summary>
    /// create a new task entity
    /// </summary>
    /// <param name="item">task to add</param>
    /// <returns>the id of the task that has been added</returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;//create a running number id
        Task newItem = new Task();
        newItem = item;
        newItem.Id = newId;
        DataSource.Tasks.Add(newItem);
        return newId;
    }
    /// <summary>
    /// delete an task entity
    /// </summary>
    /// <param name="id">the id of the task to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        //checks if the task is in the list
        Task? objectToDelete = DataSource.Tasks.FirstOrDefault(obj => obj.Id == id);
        if (objectToDelete != null)
        {
            DataSource.Tasks.Remove(objectToDelete);
        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Task עם ID {id} לא קיים");

        }
    }
    /// <summary>
    /// reads a certain task 
    /// </summary>
    /// <param name="id">the id of the task to read</param>
    /// <returns>the task with the id</returns>
    public Task? Read(int id)
    {
        //checks if the task is in the list
        Task? objectToRead = DataSource.Tasks.FirstOrDefault(obj => obj.Id == id);
        if (objectToRead != null)
        {
            return objectToRead;
        }
        else
        {
            return null;
        }
    }

    /// /// <summary>
    /// reads all the list of tasks /all the tasks that are true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>a list of items that are true to the condition</returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return from t in DataSource.Tasks
                   where filter(t)
                   select t;
        }

        List<Task> newTaskList = new List<Task  >();

        foreach (Task item in DataSource.Tasks)
        {
            Task newItem = new Task();
            newItem.Id = item.Id;
            newItem.Remarks = item.Remarks;
            newItem.Complete = item.Complete;
            newItem.Start = item.Start;
            newItem.Alias = item.Alias;
            newItem.Deliverables = item.Deliverables;
            newItem.ComplexityLevel = item.ComplexityLevel;
            newItem.CreatedAt = item.CreatedAt;
            newItem.DeadLine = item.DeadLine;
            newItem.Milestone = item.Milestone;
            newItem.Description = item.Description;
            newItem.Engineerid = item.Engineerid;
            newItem.ScheduleDate = item.ScheduleDate;
            newTaskList.Add(newItem);
        }
        return newTaskList;
    }

    /// /// /// <summary>
    /// updated a specific task
    /// </summary>
    /// <param name="item">the item for the update</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Task item)
    {
        Task? objectToDelete = DataSource.Tasks.FirstOrDefault(obj => obj.Id == item.Id);

        if (objectToDelete != null)
        {
            DataSource.Tasks.Remove(objectToDelete);
            DataSource.Tasks.Add(item);
        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Task עם ID {item.Id} לא קיים");
        }
    }
    /// <summary>
    /// reads the first task that true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>the first task that is true to the condition</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(task => filter(task));
    }

}
