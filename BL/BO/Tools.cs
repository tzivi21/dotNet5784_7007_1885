

using System.Reflection;


namespace BO;

public static class Tools
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();//to use the BL methods
    private static readonly DalApi.IDal _dal = DalApi.Factory.Get;//to use the DAL methods


    /// <summary>
    /// Rename the milestones alias to be the ids of the dependent on tasks
    /// </summary>
    /// <param name="dependenciesList"></param>
    public static void renameMilestonesAlias(List<DO.Dependency> dependenciesList)
    {
        if(dependenciesList.Count == 0) return;
        List<DO.Task?> allTasks = _dal.Task.ReadAll().ToList();
        foreach (var task in allTasks)
        {
            if (task?.Milestone is true)
            {
                string alias = "M";
                var dependsOnTaskList = dependenciesList.Where(dep => dep?.DependentTask == task.Id)
                .Select(dep => dep?.DependsOnTask).ToList();
                foreach (var dependsOnTask in dependsOnTaskList)
                {
                    alias += $" {dependsOnTask}";
                }
                task.Alias = alias;
                _dal.Task.Update(task);
            }

        }

    }


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


    public static List<DO.Dependency> CreateMileStone(List<DO.Dependency>? dependencies)
    {
        // Check for null dependencies
        if (dependencies == null)
        {
            // Return an empty list if dependencies are null
            return new List<DO.Dependency>();
        }

        int count = 0; // ID counter for the milestones

        // Create the first basic milestone for tasks that don't have dependencies
        DO.Task firstMilestone = new()
        {
            Alias = $"M{count}",
            Description = $"MStart",
            CreatedAt = DateTime.Now,
            Milestone = true,
        };
        count++;
        int idFirstMilestone = _dal.Task.Create(firstMilestone);

        List<DO.Dependency> newDependencies = new List<DO.Dependency>(); 

        // Group dependencies by the DependentTask
        var list = dependencies.GroupBy(d => d.DependentTask).ToList();
        //a list of task that nothing depends on them 
        List<int> tasksThatSomethingDependOnThem = new();

        // Iterate through tasks to find those without dependencies
        foreach (DO.Task task in _dal.Task.ReadAll())
        {
            // Check if the task has any dependencies
            var isTaskIdInList = list.Any(group => group.Any(item => item.DependentTask == task.Id));

            // The task doesn't have dependencies
            if (!isTaskIdInList)
            {
                // Create a dependency on the basic milestone
                newDependencies.Add(new DO.Dependency()
                {
                    DependentTask = task.Id,
                    DependsOnTask = idFirstMilestone
                });
            }
        }

        // Iterate through grouped dependencies to create milestones and dependencies
        foreach (var group in list)
        {
            bool flagAlreadyExistMilestone = false;
            //check if this milestone is not already exist
            var allMilestones=_dal.Task.ReadAll().Where(task=>task!.Milestone).ToList();
            int id = 0;
            //go through all milestones and check if their dependencies are equal
            foreach (var m in allMilestones)
            {
                List<DO.Dependency> allDependencies = _dal.Dependency.ReadAll()?.Where(m => m!.DependentTask == m.Id)?.ToList();
                flagAlreadyExistMilestone = AreGroupsEqual(allDependencies, group.ToList());
                if(flagAlreadyExistMilestone)
                {
                    id = m!.Id; break;

                }

            }
            if (!flagAlreadyExistMilestone)
            {
                // Create a milestone
                DO.Task milestone = new()
                {
                    Alias = $"M{count}",
                    Description = $"Milestone {count}",
                    CreatedAt = DateTime.Now,
                    Milestone = true,
                };
                count++;
                id = _dal.Task.Create(milestone);

            }
           
            foreach (var depend in group)
            {
                // Add dependencies of the tasks that are included in the milestone to the milestone
                newDependencies.Add(new DO.Dependency()
                {
                    // The ID of the current milestone
                    DependentTask = id,
                    DependsOnTask = depend.DependsOnTask
                });
                tasksThatSomethingDependOnThem.Add(depend.DependsOnTask);

            }

            // Add a new dependency of the current task in the milestone
            newDependencies.Add(new DO.Dependency()
            {
                DependentTask = group.Key,
                DependsOnTask = id
            });
        }

        // Create the end milestone for tasks that have dependencies
        DO.Task endMilestone = new()
        {
            Alias = $"M{count++}",
            Description = $"MEnd",
            CreatedAt = DateTime.Now,
            Milestone = true,
        };

        int idEndMilestone = _dal.Task.Create(endMilestone);

        // Check for tasks in the program
        List<DO.Task> allTasks = _dal.Task.ReadAll().ToList();
        if (allTasks.Count == 0)
        {
            // Throw an exception if there are no tasks in the program
            throw new BO.BlNullPropertyException("There are no tasks in the program");
        }

        List<int> allTasksId = allTasks.Where(task=>!task.Milestone).Select(task => task.Id).ToList();

        // Find tasks that nothing depends on them and add them as dependencies to the end milestone
        List<int> tasksThatNothingDependsOnThem = allTasksId.Except(tasksThatSomethingDependOnThem).ToList();
        foreach (var taskId in tasksThatNothingDependsOnThem)
        {
            newDependencies.Add(new DO.Dependency()
            {
                DependentTask = idEndMilestone,
                DependsOnTask = taskId
            });
        }

        // Return the new dependencies list
        return newDependencies;
    }
    /// <summary>
    /// Checks if two groups resulting from a GroupBy operation are equal.
    /// </summary>
    /// <typeparam name="YourItemType">The type of items in the groups.</typeparam>
    /// <param name="group1">First group to compare.</param>
    /// <param name="group2">Second group to compare.</param>
    /// <returns>True if the groups are equal, false otherwise.</returns>
    public static bool AreGroupsEqual(List<DO.Dependency>? list1, List<DO.Dependency>? list2)
    {
        if(list1 == null && list2 == null)
        {
            return true;
        }
        if((list1 == null &&list2!=null)|| (list2 == null && list1 != null)) { return false; }
        // Check if the lists are the same instance or both are null
        if (ReferenceEquals(list1, list2))
            return true;

        // Check if any of the lists is null or their counts are different
        if (list1 == null || list2 == null || list1.Count != list2.Count)
            return false;

        // Sort both lists to ensure the order of elements is the same for comparison
        list1.Sort((d1, d2) => _ = (d1.Id.CompareTo(d2.Id))) ;
        list2.Sort((d1, d2) => _ = (d1.Id.CompareTo(d2.Id)));

        // Compare each element in both lists
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i].Id != list2[i].Id)
                return false; // Return false if any elements are not equal
        }

        return true; // All elements are equal
    }

    /// <summary>
    /// Recursively going (from end to start) over the tasks and updating the deadline of each task
    /// </summary>
    /// <param name="idOfTask">The current task id</param>
    /// <param name="idOfStartMilestone"></param>
    /// <param name="dependenciesList"></param>
    /// <exception cref="BO.BlNullException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public static void updateDeadLineDate(int? idOfTask, int idOfStartMilestone, List<DO.Dependency?> dependenciesList)
    {
        //Stop condition
        if (idOfTask == idOfStartMilestone)
            return;
        //The data of current checked task
        DO.Task? dependentTask = _dal.Task.Read(idOfTask ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));


        var DependsOnTaskList = dependenciesList.Where(dep => dep?.DependentTask == idOfTask)
            .Select(dep => dep?.DependsOnTask).ToList();

        foreach (int? taskId in DependsOnTaskList)
        {
            DO.Task? currentTask = _dal.Task.Read(taskId ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

            if (currentTask is null)
                throw new BO.BlDoesNotExistException($"task with id {taskId} is not exist");

            DateTime? deadLineDate = dependentTask?.DeadLine - dependentTask?.RequiredEffortTime;

            if (currentTask.Milestone is true)
                if (currentTask.DeadLine is null || deadLineDate < currentTask.DeadLine)
                    currentTask.DeadLine = deadLineDate;
                else
                    currentTask.DeadLine = deadLineDate;
            _dal.Task.Update(currentTask);
            updateDeadLineDate(taskId, idOfStartMilestone, dependenciesList);
        }

    }
    /// <summary>
    /// Recursively going (from start to end) over the tasks and updating the Scheduled Date of each task
    /// </summary>
    /// <param name="idOfTask">The current task id</param>
    /// <param name="idOfEndMilestone"></param>
    /// <param name="dependenciesList"></param>
    /// <exception cref="BO.BlNullException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="BlPlanningOfProjectTimesException"></exception>
    public static void updateScheduledDate(int? idOfTask, int idOfEndMilestone, List<DO.Dependency?> dependenciesList)
    {
        //Stop condition
        if (idOfTask == idOfEndMilestone)
            return;

        //The data of current checked task
        DO.Task? dependentTask = _dal.Task.Read(idOfTask ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

        var DependentTaskList = dependenciesList.Where(dep => dep?.DependsOnTask == idOfTask)
            .Select(dep => dep?.DependentTask).ToList();

        foreach (int? taskId in DependentTaskList)
        {
            DO.Task? currentTask = _dal.Task.Read(taskId ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

            if (currentTask is null)
                throw new BO.BlDoesNotExistException($"task with id {taskId} is not exist");
            if (dependentTask?.DeadLine + currentTask.RequiredEffortTime > currentTask.DeadLine)
                throw new BlPlanningOfProjectTimesException($"According to the date restrictions, the task {taskId} does not have time to be completed in its entirety");
            currentTask.ScheduleDate = dependentTask?.DeadLine;


            updateDeadLineDate(taskId, idOfEndMilestone, dependenciesList);
        }
    }

}
