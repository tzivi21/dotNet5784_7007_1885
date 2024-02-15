namespace Dal;
using DalApi;
sealed internal class DalList : IDal
{
    public static DalList Instance { get; } = new();
    public IDependency Dependency =>  new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();
    public DateTime? EndProjectDate
    {
        get => DataSource.Config.endProjectDate;
    }
    public DateTime? StartProjectDate
    {
        get => DataSource.Config.startProjectDate;
    }

    public void Reset()
    {
        DataSource.Tasks!.Clear();
        DataSource.Engineers!.Clear();
        DataSource.Dependencies!.Clear();

    }
}
