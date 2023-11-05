

namespace DO;
/// <summary>
/// A class that represents the dependencies of each task
/// <param name="Id">the defining number</param>
/// <param name="DependentTask">the id of the task that is dependent</param>
/// <param name="DependsOnTask">the id of the task it is depends on</param>
/// </summary>
public record Dependency
{
    public int Id { set; get; }
    int DependentTask;
    int DependsOnTask;
    public Dependency(int myDependentTask, int myDependsOnTask)
    {
        DependentTask = myDependentTask;
        DependentTask = myDependsOnTask;

    }
    public Dependency() 
    {
    } 
    

}
