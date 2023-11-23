namespace Dal;
using System.Collections.Generic;
using DalApi;
using DO;
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// create a new dependency entity
    /// </summary>
    /// <param name="item">dependency to add</param>
    /// <returns>the id of the dependency that has been added</returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;//create a new running number
        Dependency newItem = new Dependency();
        newItem = item;
        newItem.Id= newId;
        DataSource.Dependencies.Add(newItem);
        return newId;

    }
    /// <summary>
    /// delete an dependency entity
    /// </summary>
    /// <param name="id">the id of the dependency to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
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
    /// <summary>
    /// reads a certaun dependency 
    /// </summary>
    /// <param name="id">the id of the dependency to read</param>
    /// <returns>the dependency with the id</returns>
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

    /// /// <summary>
    /// reads all the list of dependencies /all the dependencies that are true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>a list of items that are true to the condition</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null) {
        if (filter != null)
        {
            return from d in DataSource.Dependencies
                   where filter(d)
                   select d;
        }
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

    /// /// <summary>
    /// updated a specific dependency
    /// </summary>
    /// <param name="item">the item for the update</param>
    /// <exception cref="DalDoesNotExistException"></exception>
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
    /// <summary>
    /// reads the first dependency that true to the condition
    /// </summary>
    /// <param name="filter">lamda function who checks the cindition</param>
    /// <returns>the first dependency that is true to the condition</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(dependency => filter(dependency));
    }
}
