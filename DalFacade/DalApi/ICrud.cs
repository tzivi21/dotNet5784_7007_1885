
using DO;

namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates new entity object in DAL
    T? Read(int id); //Reads entity object by its ID 
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); //get all the objects from the list that are right for a given condition
    void Update(T item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
    T? Read(Func<T, bool> filter);//gets a condition and returns the object that Upholds the condition from the list 
}
