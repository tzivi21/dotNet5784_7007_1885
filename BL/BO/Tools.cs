

using System.Reflection;


namespace BO;

public static class Tools
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();//to use the BL methods


    private static readonly DalApi.IDal _dal = DalApi.Factory.Get;//to use the DAL methods

    /// <summary>
    /// Returns a string representation of the properties and their values of the provided object.
    /// </summary>
    /// <param name="obj">The object whose properties will be retrieved.</param>
    /// <returns>A string containing the object's properties and values.</returns>
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

    /// <summary>
    /// Determines the status of a task based on its Start, DeadLine, and Complete properties.
    /// </summary>
    /// <param name="task">The task to determine the status for.</param>
    /// <returns>The status of the task based on specific conditions.</returns>
    public static Status DetermineStatus(DO.Task task)
    {
        if (task.Start == null && task.DeadLine == null)
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
        else if (task.Complete <= DateTime.Now)
        {
            return Status.Done;
        }
        else
        {
            return Status.Scheduled;
        }
    }

    /// <summary>
    /// Calculates the completion percentage of a list of tasks.
    /// </summary>
    /// <param name="listTask">The list of tasks for which completion percentage will be calculated.</param>
    /// <returns>The calculated completion percentage.</returns>
    public static double CompletionPercentage(List<BO.TaskInList> listTask)
    {
        double sum = 0;
        for (int i = 0; i < listTask.Count; i++)
        {
            sum += (int)listTask[i].Status * 100 / 4;
        }
        return sum / listTask.Count;
    }


    public static List<DO.Dependency> CreateMileStone(List<DO.Dependency> dependencies)
    {
        int count = 0;//id counter for the milestones
        DO.Task firstMilestone = new()//the first basic milestone for tasks who doesn't have dependencies
        {
            Alias = $"M{count++}",
            Description = $"MStart",
            CreatedAt = DateTime.Now,
            Milestone = true,
        };
        int idFirstMilestone = _dal.Task.Create(firstMilestone);

        List<DO.Dependency> newDependencies = new List<DO.Dependency>();//all the dependencies in this program
        //all the dependencies in groups by the DependentTask
        var list = dependencies.GroupBy(d => d.DependentTask).ToList();
        //the list of dependencies sorted
        var sortedList = list.OrderBy(comparer => comparer);
        foreach (DO.Task task in _dal.Task.ReadAll())
        {
            //check if the task has any dependencies
            var isTaskIdInList = sortedList.Any(group => group.Key == task.Id);
            //the task doesn't have dependencies
            if (!isTaskIdInList)
            {
                //create a dependency on the basic milestone
                newDependencies.Add(new DO.Dependency()
                {
                    DependentTask = task.Id,
                    DependsOnTask = idFirstMilestone
                });
            }

        }
        foreach (var group in list)
        {
            //create a milestone
            DO.Task milestone = new()
            {
                Alias = $"M{count++}",
                Description = $"M{count++}",
                CreatedAt = DateTime.Now,
                Milestone = true,
            };
            int id = _dal.Task.Create(milestone);
            foreach (var depend in group)
            {
                //adds dependencies of the tasks that are included in the milestone to the milestone
                newDependencies.Add(new DO.Dependency()
                {
                    //the id of the current milestone
                    DependentTask = id,
                    DependsOnTask = depend.DependsOnTask
                });
            }
            //adds a new dependency of the current task in the milestone
            newDependencies.Add(new DO.Dependency()
            {
                DependentTask = group.Key,
                DependsOnTask = id
            });


        }

        //return the new dependencies list union with the all dependencies list
        return newDependencies;
    }

}
