

namespace BIApi;

public interface IEngineer
{
    /// <summary>
    /// adds a new engineer
    /// </summary>
    /// <param name="item">the engineer information to add</param>
    public int Add(BO.Engineer item);
    /// <summary>
    /// return the information of all engineers
    /// </summary>
    /// <returns>an enumerable list of all engineers</returns>
    public BO.Engineer? Read(int id);
    /// <summary>
    /// return a specific engineer's information
    /// </summary>
    /// <param name="id">the id of the wanted engineer</param>
    /// <returns>engineer object with the given id</returns>

    public IEnumerable<BO.Engineer> ReadAll();
    /// <summary>
    /// update a specific engineer
    /// </summary>
    /// <param name="item">the engineer with the information to update</param>
    public void Update(BO.Engineer item);
    /// <summary>
    /// deletes a specific engineer
    /// </summary>
    /// <param name="id">the id of the engineer to delete</param>
    public void Delete(int id);

}
