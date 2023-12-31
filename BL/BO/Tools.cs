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
        if (dependenciesList.Count == 0) return;
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

    /// <summary>
    /// creates the milestones and dependencies
    /// </summary>
    /// <param name="dependencies">all the current dependencies</param>
    /// <returns>return the new dependencies list</returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public static List<DO.Dependency> CreateMileStone(List<DO.Dependency>? dependencies)
    {
        // Check for null dependencies
        if (dependencies == null)
        {
            // Return an empty list if dependencies are null
            return new List<DO.Dependency>();
        }

        int count = 0; // ID counter for the milestones
        List<DO.Dependency> newDependencies = new List<DO.Dependency>();
        List<int> keysOfGroupsToDelete = new List<int>();
        // Group dependencies by the DependentTask
        var list = dependencies.GroupBy(d => d.DependentTask).ToList();

        //a list of task that something depends on them 
        List<int> tasksThatDependsOnSomething = new();
        #region create start milestone and it's dependencies
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

        

        // Iterate through tasks to find those without dependencies
        foreach (DO.Task task in _dal.Task.ReadAll())
        {
            // Check if the task has any dependencies
            var isTaskIdInList = list.Any(group => group.Any(item => item.DependentTask == task.Id));

            // The task doesn't have dependencies
            if (!isTaskIdInList&&!task!.Milestone)
            {
                // Create a dependency on the basic milestone
                newDependencies.Add(new DO.Dependency()
                {
                    DependentTask = task.Id,
                    DependsOnTask = idFirstMilestone
                });
                tasksThatDependsOnSomething.Add(task.Id);
            }
        }
        #endregion
        // Iterate through grouped dependencies to create milestones and dependencies
        foreach (var group in list)
        {
            
            if (keysOfGroupsToDelete.Select(key => key).Where(key => key == group.Key).ToList().Count == 0)
            {
                #region checking of same groups with different key
                //checks if the group has another task that all the group items depends on it
                List<int> sameDependentOnTaskGroups = new();
                //iterate through the the grouping list and check for same groups(compare depends on task)
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    if (list[i].Key != group.Key)
                    {
                        var group1 = list[i];
                        var group2 = group;

                        // Extract DependentOnTask values for each group and compare
                        var dependentOnTasks1 = group1.Select(dep => dep.DependsOnTask).ToList();
                        var dependentOnTasks2 = group2.Select(dep => dep.DependsOnTask).ToList();

                        // Compare the lists of DependentOnTask values
                        bool areDependentOnTasksEqual = dependentOnTasks1.SequenceEqual(dependentOnTasks2);


                        if (areDependentOnTasksEqual)
                        {
                            //adds the key of the group to the list
                            sameDependentOnTaskGroups.Add(list[i].Key);
                            //removes the proup from the list of groups
                            keysOfGroupsToDelete.Add(list[i].Key);
                        }

                    }

                }
                #endregion
                #region check for already existing milestone
                //checks if the milestone is already exist
                bool flagAlreadyExistMilestone = false;
                //check if this milestone is not already exist
                var allMilestones = _dal.Task.ReadAll().Where(task => task!.Milestone).ToList();
                int id = 0;
                //go through all milestones and check if their dependencies are equal
                foreach (var m in allMilestones)
                {
                    List<DO.Dependency> allDependencies = _dal.Dependency.ReadAll()?.Where(ms => ms!.DependentTask == m.Id)?.ToList();
                    flagAlreadyExistMilestone = AreGroupsEqual(allDependencies, group.ToList());
                    if (flagAlreadyExistMilestone)
                    {
                        id = m!.Id; break;

                    }

                }
                #endregion
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
                #region create proper dependencies
                foreach (var depend in group)
                {
                    // Add dependencies of the tasks that are included in the milestone to the milestone
                    newDependencies.Add(new DO.Dependency()
                    {
                        // The ID of the current milestone
                        DependentTask = id,
                        DependsOnTask = depend.DependsOnTask
                    });
                    tasksThatDependsOnSomething.Add(depend.DependsOnTask);

                }

                // Add a new dependency of the current task in the milestone
                newDependencies.Add(new DO.Dependency()
                {
                    DependentTask = group.Key,
                    DependsOnTask = id
                });
                if (sameDependentOnTaskGroups.Count > 0)
                {
                    foreach (var depend in sameDependentOnTaskGroups)
                    {
                        newDependencies.Add(new DO.Dependency()
                        {
                            DependentTask = depend,
                            DependsOnTask = id
                        });
                    }
                }
                #endregion
            }

        }
        #region create end milestone and it's dependencies
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

        List<int> allTasksId = allTasks.Where(task => !task.Milestone).Select(task => task.Id).ToList();

        // Find tasks that nothing depends on them and add them as dependencies to the end milestone
        List<int> tasksThatNothingDependsOnThem = allTasksId.Except(tasksThatDependsOnSomething).ToList();
        foreach (var taskId in tasksThatNothingDependsOnThem)
        {
            newDependencies.Add(new DO.Dependency()
            {
                DependentTask = idEndMilestone,
                DependsOnTask = taskId
            });
        }
        #endregion
        // Return the new dependencies list
        return newDependencies;
    }
    /// <summary>
    /// Checks if two groups resulting from a GroupBy operation are equal.
    /// </summary>
    /// <typeparam name="DO.Dependency">The type of items in the groups.</typeparam>
    /// <param name="group1">First group to compare.</param>
    /// <param name="group2">Second group to compare.</param>
    /// <returns>True if the groups are equal, false otherwise.</returns>
    public static bool AreGroupsEqual(List<DO.Dependency>? list1, List<DO.Dependency>? list2)
    {
        if (list1 == null && list2 == null)
        {
            return true;
        }
        if ((list1 == null && list2 != null) || (list2 == null && list1 != null)) { return false; }
        // Check if the lists are the same instance or both are null
        if (ReferenceEquals(list1, list2))
            return true;

        // Check if any of the lists is null or their counts are different
        if (list1 == null || list2 == null || list1.Count != list2.Count)
            return false;

        // Sort both lists to ensure the order of elements is the same for comparison
        list1.Sort((d1, d2) => _ = (d1.Id.CompareTo(d2.Id)));
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

        if (DependsOnTaskList is not null)
        {
            foreach (int? taskId in DependsOnTaskList)
            {
                DO.Task? currentTask = _dal.Task.Read(taskId ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

                if (currentTask is null)
                    throw new BO.BlDoesNotExistException($"task with id {taskId} is not exist");

                DateTime? deadLineDate = dependentTask?.DeadLine - dependentTask?.RequiredEffortTime;


                if (currentTask.Milestone is false || (currentTask.Milestone is true && (currentTask.DeadLine is null || deadLineDate < currentTask.DeadLine)))
                    currentTask.DeadLine = deadLineDate;

                _dal.Task.Update(currentTask);

                updateDeadLineDate(taskId, idOfStartMilestone, dependenciesList);
            }
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

            DateTime? scheduledDate = dependentTask?.ScheduleDate + dependentTask?.RequiredEffortTime;

            if (currentTask.Milestone is false || (currentTask.Milestone is true && (currentTask.ScheduleDate is null || scheduledDate > currentTask.ScheduleDate)))
                currentTask.ScheduleDate = scheduledDate;

            _dal.Task.Update(currentTask);

            updateScheduledDate(taskId, idOfEndMilestone, dependenciesList);
        }
    }
    
}
