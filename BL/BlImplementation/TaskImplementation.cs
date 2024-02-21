
using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        if(item.Dependencies!=null)
        {
            foreach (var dep in item.Dependencies)
            {
                DO.Dependency dependency=new DO.Dependency()
                {
                    DependentTask=dep.Id,
                    DependsOnTask=item.Id
                };
                _dal.Dependency.Create(dependency);
            }
        }
        DO.Task DOTask = new
            (item.Description,
            item.Alias,
            item.RequiredEffortTime,
            item.StartDate,
            item.ScheduledStartDate,
            item.DeadlineDate,
            item.CompleteDate,
            item.Deliverables,
            item.Remarks,
            item.Engineer?.Id??null,
            (DO.EngineerExperience)item.ComplexityLevel!);
        try
        {
            int id = _dal.Task.Create(DOTask);
            return id;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"task with ID={item.Id} already exists", ex);
        }


    }

    public void Delete(int id)
    {
        //check that no task are dependent on this task
            if (Read(id)!.Dependencies!.Count > 0)
            {
                throw new BO.BlDeletionImpossible($"The task with Id={id} can't be deleted");
            }
            try
            {
                _dal.Task.Delete(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlAlreadyExistException($"task with ID={id} already exists", ex);
            }
        


    }

    public BO.Task? Read(int id)
    {
        DO.Task? task = _dal.Task.Read(id);
        if (task == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");
        }
        else
        {
            // Create the list of dependencies 
            List<BO.TaskInList> dependencies = _dal.Dependency!.ReadAll(d => d.DependentTask == id)
                .Select(d => new BO.TaskInList()
                {
                    Id = d.DependsOnTask,
                    Alias = _dal.Task!.Read(d.DependsOnTask)?.Alias ?? "",
                    Description = _dal.Task!.Read(d.DependsOnTask)?.Description ?? "",
                    Status = Tools.DetermineStatus(_dal.Task!.Read(d.DependsOnTask))
                })
                .ToList();

            // Calculate the status
            Status status = Tools.DetermineStatus(task);

            // Search and create the engineer details that work on this task
            EngineerInTask engineer = new BO.EngineerInTask();
            var engineerTask = _dal.Engineer!.Read(task.Engineerid ?? 0);
            if (engineerTask != null)
            {
                engineer.Id = engineerTask.Id;
                engineer.Name = engineerTask.Name ?? "";
            }

            // Check which of the dependencies is the milestone of this task
            MilestoneInTask? milestone = dependencies
                .Where(d => _dal.Task!.Read(d.Id)?.Milestone == true)
                .Select(d => new BO.MilestoneInTask()
                {
                    Id = d.Id,
                    Alias = d.Alias
                })
                .FirstOrDefault();

            return new BO.Task()
            {
                Id = id,
                Description = task.Description,
                Alias = task.Alias,
                RequiredEffortTime = task.RequiredEffortTime,
                CreatedAtDate = task.CreatedAt,
                Status = status,
                Dependencies = dependencies,
                Milestone = milestone,
                StartDate = task.Start,
                ScheduledStartDate = task.ScheduleDate,
                ForeCastDate = task.Start?.Add(task.RequiredEffortTime ?? TimeSpan.Zero),
                DeadlineDate = task.DeadLine,
                CompleteDate = task.Complete,
                Deliverables = task.Deliverables,
                Remarks = task.Remarks,
                Engineer = engineer,
                ComplexityLevel = (BO.EngineerExperience)task.ComplexityLevel!
            };
        }
    }


    public IEnumerable<BO.Task> ReadAll(Func<BO.Task,bool>? condition=null)
    {
        var allTasks= from t in _dal.Task.ReadAll()
                      where t.Milestone == false
                      select Read(t.Id);
        if (condition != null)
        {
            return allTasks.Where(t => condition(t));
        }
        return  allTasks;
    }

    public void Update(BO.Task item)
    {
        if (_dal.Task.Read(item.Id) == null)
            throw new BO.BlDoesNotExistException($"task with ID={item.Id} does Not exist");
        try
        {
            BO.Task currentTask=Read(item.Id)!;
            if (currentTask.Dependencies != null&&item.Dependencies!=null)
            {
                List<BO.TaskInList> dependenciesToAdd = currentTask.Dependencies.Except(item.Dependencies).ToList();
                if (dependenciesToAdd.Count != 0)
                {
                    foreach (var dep in dependenciesToAdd)
                    {
                        DO.Dependency dependency = new DO.Dependency()
                        {
                            DependentTask = dep.Id,
                            DependsOnTask = item.Id
                        };
                        _dal.Dependency.Create(dependency);
                    }
                }

            }
            DO.Task updatedTask = new DO.Task()
            {
                Id = item.Id,
                Description = item.Description,
                Alias = item.Alias,
                RequiredEffortTime = item.RequiredEffortTime,
                CreatedAt = item.CreatedAtDate,
                Start = item.StartDate,
                ScheduleDate = item.ScheduledStartDate,
                DeadLine = item.DeadlineDate,
                Complete = item.CompleteDate,
                Deliverables = item.Deliverables,
                Remarks = item.Remarks,
                Engineerid = item.Engineer.Id,
                ComplexityLevel = (DO.EngineerExperience)item.ComplexityLevel!
            };
            _dal.Task!.Update(updatedTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"task with ID={item.Id} does Not exist",ex);

        }
    }
}
