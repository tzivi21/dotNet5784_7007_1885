

namespace DO;
/// <summary>
/// A class that represents the dependencies of each task
/// <param name="Id">the defining number</param>
/// <param name="DependentTask">the id of the task that is dependent</param>
/// <param name="DependsOnTask">the id of the task it is depends on</param>
/// </summary>
public record Dependency
{
    public static int nextId = 1;
    public int Id { set; get; }
    public int DependentTask { set; get; }
    public int DependsOnTask { set; get; }
    public Dependency(int myDependentTask, int myDependsOnTask)
    {
        DependentTask = myDependentTask;
        DependentTask = myDependsOnTask;
        //Id=;
    }
    public Dependency() 
    {
        //Id=nextId++;
    } 
    

}
