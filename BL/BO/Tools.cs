
using System.Reflection;

namespace BO;

public static class Tools
{
    public static string ToStringProperty(object obj)
    {
        PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        string output = $"{obj.GetType().Name}: ";

        foreach (PropertyInfo property in properties)
        {
            output += $"{property.Name}: {property.GetValue(obj)}, ";
        }

        return output.TrimEnd(',', ' ');

    }
    public static Status DetermineStatus(DO.Task task)
    {
        if (task.Start == null  && task.DeadLine == null)
        {
            return Status.Unscheduled;
        }
        else if (task.Start != null && task.DeadLine != null && task.Complete == null)
        {
            if (DateTime.Now < task.DeadLine)
            {
                return Status.OnTrack;
            }
            else
            {
                return Status.InJeopardy;
            }
        }
        else if (task.Complete <=DateTime.Now)
        {
            return Status.Done;
        }
        else
        {
            return Status.Scheduled;
        }
    }
}
