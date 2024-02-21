

namespace BlApi;

public interface ITask
{
    /// <summary>
    /// adds a new task
    /// </summary>
    /// <param name="item">the task information to add</param>
    public int Create(BO.Task item);
    /// <summary>
    /// return a specific task's information
    /// </summary>
    /// <param name="id">the id of the wanted task</param>
    /// <returns>Task object with the given id</returns>
    public BO.Task? Read(int id);
    /// <summary>
    /// return the information of all tasks
    /// </summary>
    /// <returns>an enumerable list of all tasks</returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? condition = null);
    /// <summary>
    /// update a specific task
    /// </summary>
    /// <param name="item">the task with the information to update</param>
    public void Update(BO.Task item);
    /// <summary>
    /// deletes a specific task
    /// </summary>
    /// <param name="id">the id of the task to delete</param>
    public void Delete(int id);


}
