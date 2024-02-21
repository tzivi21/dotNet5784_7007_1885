namespace DalApi;

public interface IDal
{
    IDependency Dependency { get; }
    ITask Task { get; }
    IEngineer Engineer { get; }

    public DateTime? StartProjectDate { get;}
    public DateTime? EndProjectDate { get; }
    void Reset();
    public void ResetConfiguration();


}
