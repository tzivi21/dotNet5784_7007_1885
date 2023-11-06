namespace Dal;

using System;
using System.Collections.Generic;
using DalApi;
using DO;


public class EngineerImplementation : IEngineer
{

    public int Create(Engineer item)
    {
        // בודק אם כבר יש ברשימה אדם עם אותו ID
        if (DataSource.Engineers.Exists(p => p.Id == item.Id))
        {
            throw new Exception($"אובייקט מסוג Person עם ID {item.Id} כבר קיים");
        }

        // אם לא קיים אדם עם אותו ID, הוסף את האדם לרשימה
        DataSource.Engineers.Add(item);

        // החזר את ה-ID של האדם החדש
        return item.Id;

    }


    public void Delete(int id)
    {
        // בודק אם כבר יש ברשימה אדם עם אותו ID
        Engineer objectToDelete = DataSource.Engineers.FirstOrDefault(obj => obj.Id == id);‏
        if (objectToDelete != null)
        {
            DataSource.Engineers.Remove(objectToDelete);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Person עם ID {id} לא קיים");

        }
    }

    public Engineer? Read(int id)
    {
        Engineer objectToRead = DataSource.Engineers.FirstOrDefault(obj => obj.Id == id);
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
                  newItem = item;
                    // Create a new instance of the object
                    // Copy the properties or fields from the original item to the new item
                    // Example: newItem.Property = item.Property;
                  newEngineerList.Add(newItem);
                }
            return newEngineerList;

    }

public void Update(Engineer item)
    {
        Engineer objectToDelete = DataSource.Engineers.FirstOrDefault(obj => obj.Id == item.Id);

        if (objectToDelete != null)
        {
            DataSource.Engineers.Remove(objectToDelete);
            DataSource.Engineers.Add(item);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Person עם ID {item.Id} לא קיים");
        }
    }
}


