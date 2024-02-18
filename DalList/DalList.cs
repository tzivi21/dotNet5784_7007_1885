namespace Dal;
using DalApi;
using System.Globalization;

sealed internal class DalList : IDal
{
    public static DalList Instance { get; } = new();
    public IDependency Dependency =>  new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();
    public DateTime? EndProjectDate
    {
        get
        {
            if (DateTime.TryParseExact(DataSource.Config.endProjectDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                return endDate;
            }
            return null;
        }
    }

    public DateTime? StartProjectDate
    {
        get
        {
            if (DateTime.TryParseExact(DataSource.Config.startProjectDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                return startDate;
            }
            return null;
        }
    }


    public void Reset()
    {
        DataSource.Tasks!.Clear();
        DataSource.Engineers!.Clear();
        DataSource.Dependencies!.Clear();

    }
}
