using System.Globalization;

namespace Dal;

public static class DataSource
{
    internal static List<DO.Engineer>? Engineers { get; } = new();
    internal static List<DO.Task>? Tasks { get; } = new();
    internal static List<DO.Dependency>? Dependencies { get; } = new();
    public static class Config
    {
        internal const int startTaskId = 100;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get { return nextTaskId++; } }
        internal const int startDependencyId = 250;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get { return nextDependencyId++; } }
        internal static string startProjectDate = "2020-01-01T12:00:00";
        internal static string endProjectDate = "2028-01-01T12:00:00";


    }
}
