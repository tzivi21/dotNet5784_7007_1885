
using BlApi;
using BO;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public Milestone? Read(int id)
    {
        try
        {
            //read the milestone(do.task that presents the milestone)
            DO.Task? DOTask = _dal.Task.Read(id);
            //create the dependencies list of this milestone
            List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependentTask == id)
                                                where true
                                                select new BO.TaskInList()
                                                {
                                                    Id = d.DependsOnTask,
                                                    Alias = _dal.Task!.Read(d.DependsOnTask)?.Alias??"",
                                                    Description = _dal.Task!.Read(d.DependsOnTask)?.Description??"",
                                                    Status = Tools.DetermineStatus(_dal.Task!.Read(d.DependsOnTask))
                                                }
                                                ).ToList();
            return new BO.Milestone()
            {
                Id = id,
                Description = DOTask!.Description??"",
                Alias = DOTask!.Alias?? "",
                CreatedAtDate = DOTask!.CreatedAt,
                //calculates the status
                Status = Tools.DetermineStatus(DOTask),
                //for cast=the start date of the milestone +the time it requires to do it
                ForeCastDate = DOTask.Start?.Add(DOTask.RequiredEffortTime ?? new TimeSpan(0)),
                DeadlineDate = DOTask!.DeadLine,
                CompleteDate = DOTask!.Complete,
                //calculate the percentage
                CompletionPercentage = Tools.CompletionPercentage(dependencies),
                Dependencies = dependencies
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"milestone with ID={id} does not  exists", ex);
        }
    }

    public BO.Milestone Update(Milestone item)
    {
        //check if the milestone existes
        DO.Task? prevTask = _dal.Task.Read(item.Id);
        if (prevTask == null)
        {
            throw new BO.BlDoesNotExistException($"milestone with ID={item.Id} does not  exists");
        }
        //updates only the description,alias and remarks
        DO.Task DOTask = new DO.Task()
        {
            Id = item.Id,
            Description = item.Description,
            Alias = item.Alias,
            RequiredEffortTime = prevTask.RequiredEffortTime,
            CreatedAt = prevTask.CreatedAt,
            Start = prevTask.Start,
            ScheduleDate = prevTask.ScheduleDate,
            DeadLine = prevTask.DeadLine,
            Complete = prevTask.Complete,
            Deliverables = prevTask.Deliverables,
            Remarks = item.Remarks,
            Engineerid = prevTask.Engineerid,
            ComplexityLevel = prevTask.ComplexityLevel
            
        };
        try
        {
            //updates the milestone
            _dal.Task.Update(DOTask);
            //create the list of dependencies
            List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependentTask == DOTask.Id)
                                                where true
                                                select new BO.TaskInList()
                                                {
                                                    Id = d.DependsOnTask,
                                                    Alias = _dal.Task!.Read(d.DependsOnTask)?.Alias?? "",
                                                    Description = _dal.Task!.Read(d.DependsOnTask)?.Description??"",
                                                    Status = Tools.DetermineStatus(_dal.Task!.Read(d.DependsOnTask))
                                                }
            
                                                ).ToList();
            //return the updated milestone
            return new BO.Milestone()
            {
                Id = DOTask.Id,
                Description = DOTask!.Description,
                Alias = DOTask!.Alias,
                CreatedAtDate = DOTask!.CreatedAt,
                Status = Tools.DetermineStatus(DOTask),
                //ForeCastDate = DOTask!.ForCastDate,
                DeadlineDate = DOTask!.DeadLine,
                CompleteDate = DOTask!.Complete,
                CompletionPercentage = Tools.CompletionPercentage(dependencies),
                Dependencies = dependencies
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"milestone with ID={item.Id} does not  exists", ex);
        }


    }
    
}
