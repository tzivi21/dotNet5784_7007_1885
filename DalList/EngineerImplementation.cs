namespace Dal;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DalApi;
using DO;


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
        //checks if this engineer is already exists
        if (DataSource.Engineers.Exists(p => p.Id == item.Id))
        {
            throw new DalAlreadyExistException($"אובייקט מסוג Person עם ID {item.Id} כבר קיים");
        }

        //adds the engineer to list if he doesn't already exists
        DataSource.Engineers.Add(item);

        
        return item.Id;

    }
    /// <summary>
    /// delete an engineer entity
    /// </summary>
    /// <param name="id">the id of the engineer to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>

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
    /// <summary>
    /// reads a certaun engineer 
    /// </summary>
    /// <param name="id">the id of the engineer to read</param>
    /// <returns>the engineer with the id</returns>
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
   
    /// /// <summary>
    /// reads all the list of engineers /all the engineers that are true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>a list of items that are true to the condition</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return from engineer in DataSource.Engineers
                   where filter(engineer)
                   select engineer;
        }
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
    
    /// /// /// <summary>
    /// updated a specific engineer
    /// </summary>
    /// <param name="item">the item for the update</param>
    /// <exception cref="DalDoesNotExistException"></exception>
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
    /// <summary>
    /// reads the first engineer that true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>the first enginner that is true to the condition</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(engineer => filter(engineer));
    }
}


