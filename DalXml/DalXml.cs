namespace Dal;
using DalApi;

//stage 3
sealed internal class DalXml : IDal
{
    public static DalXml Instance {  get; } = new DalXml();
    private DalXml() { }
    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();
}
