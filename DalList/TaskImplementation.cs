namespace Dal;

using System;
using System.Collections.Generic;
using DalApi;
using DO;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;//create a running number id
        Task newItem = new Task();
        newItem = item;
        newItem.Id = newId;
        DataSource.Tasks.Add(newItem);
        return newId;
    }

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

    public List<Task> ReadAll()
    {

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
    
}
