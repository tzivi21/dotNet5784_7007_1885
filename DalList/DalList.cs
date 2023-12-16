namespace Dal;
using DalApi;
sealed internal class DalList : IDal
{
    public static DalList Instance { get; } = new();
    public IDependency Dependency =>  new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();
    private DalList() { }   
}
