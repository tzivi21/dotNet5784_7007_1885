namespace Dal;

using System;
using System.Collections.Generic;
using DalApi;
using DO;


internal class EngineerImplementation : IEngineer
{

    public int Create(Engineer item)
    {
        //checks if this engineer is already exists
        if (DataSource.Engineers.Exists(p => p.Id == item.Id))
        {
            throw new DalAlreadyExistException($"אובייקט מסוג Person עם ID {item.Id} כבר קיים");
        }

        //adds the engineer to list if he doesn't already exists
        DataSource.Engineers.Add(item);

        
        return item.Id;

    }


    public void Delete(int id)
    {
        //checks if this engineer is already exists
        Engineer? objectToDelete = DataSource.Engineers.FirstOrDefault(obj => obj.Id == id);
        if (objectToDelete != null)
        {
            DataSource.Engineers.Remove(objectToDelete);
        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Person עם ID {id} לא קיים");

        }
    }

    public Engineer? Read(int id)
    {
        Engineer? objectToRead = DataSource.Engineers.FirstOrDefault(obj => obj.Id == id);
        if (objectToRead != null)
        {
            return objectToRead;
        }
        else
        {
            return null;
        }
    }

    public List<Engineer> ReadAll()
    {
            List<Engineer> newEngineerList = new List<Engineer>();

            foreach (Engineer item in DataSource.Engineers)
                {
                    Engineer newItem = new Engineer();
                    newItem.Id = item.Id;
                    newItem.Name = item.Name;
                    newItem.Email = item.Email;
                    newItem.Level = item.Level;
                    newEngineerList.Add(newItem);
                }
            return newEngineerList;

    }

public void Update(Engineer item)
    {
        Engineer? objectToDelete = DataSource.Engineers.FirstOrDefault(obj => obj.Id == item.Id);

        if (objectToDelete != null)
        {
            DataSource.Engineers.Remove(objectToDelete);
            DataSource.Engineers.Add(item);
        }
        else
        {
            throw new DalDoesNotExistException($"אובייקט מסוג Person עם ID {item.Id} לא קיים");
        }
    }
}


