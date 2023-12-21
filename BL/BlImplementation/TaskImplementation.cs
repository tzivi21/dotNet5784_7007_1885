﻿
using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
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
            item.ComplexityLevel);
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
            throw new BO.BlDoesNotExistException($"task with ID={id} does Not exist");

        else
        {
            //create the list of dependencies 
            List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependentTask == id)
                                                where true
                                                select new BO.TaskInList()
                                                {
                                                    Id = d.DependsOnTask,
                                                    Alias = _dal.Task!.Read(d.DependsOnTask)?.Alias ?? "",
                                                    Description = _dal.Task!.Read(d.DependsOnTask)?.Description?? "",
                                                    Status = Tools.DetermineStatus(_dal.Task!.Read(d.DependsOnTask))
                                                }
                                                ).ToList();
            //calculates the status
            Status status = Tools.DetermineStatus(task);
            //search and create the engineer details that works on this task
            EngineerInTask engineer = new BO.EngineerInTask()
            {
                Id = _dal.Engineer!.Read(task.Engineerid??0)?.Id ?? 0,
                Name = _dal.Engineer!.Read(task.Engineerid??0)?.Name??""
            };
            //check in the dependencies which one of them is the milestone of this task
            MilestoneInTask? milestone = dependencies
                    .Where(d => _dal.Task!.Read(d.Id)!.Milestone == true)
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
                RequiredEffortTime=task.RequiredEffortTime,
                CreatedAtDate = task.CreatedAt,
                Status = status,
                Dependencies = dependencies,
                Milestone = milestone,
                StartDate = task.Start,
                ScheduledStartDate = task.ScheduleDate,
                //for cast=start time+the requiered time to finish the task
                ForeCastDate =task.Start?.Add(task.RequiredEffortTime??new TimeSpan(0)),
                DeadlineDate = task.DeadLine,
                CompleteDate = task.Complete,
                Deliverables = task.Deliverables,
                Remarks = task.Remarks,
                Engineer = engineer,
                ComplexityLevel = task.ComplexityLevel
            }




            ;
        }


    }
   
    public IEnumerable<BO.Task> ReadAll()
    {
        return from t in _dal.Task.ReadAll()
               let dependencies = _dal.Dependency!.ReadAll(d => d.DependentTask == t.Id)
                                .Select(d => new BO.TaskInList()
                                {
                                    Id = d.DependsOnTask,
                                    Alias = _dal.Task!.Read(d.DependsOnTask)?.Alias ?? "",
                                    Description = _dal.Task!.Read(d.DependsOnTask)?.Description?? "",
                                    Status = Tools.DetermineStatus(_dal.Task!.Read(d.DependsOnTask))
                                })
                                .ToList()
               select new BO.Task()
               {
                   Id = t.Id,
                   Description = t.Description,
                   Alias = t.Alias,
                   CreatedAtDate = t.CreatedAt,
                   Status = Tools.DetermineStatus(t),
                   Dependencies = dependencies,
                   Milestone = dependencies
                                .Where(d => _dal.Task!.Read(d.Id)!.Milestone == true)
                                .Select(d => new BO.MilestoneInTask()
                                {
                                    Id = d.Id,
                                    Alias = d.Alias
                                })
                                .FirstOrDefault(),
                   StartDate = t.Start,
                   ScheduledStartDate = t.ScheduleDate,
                   ForeCastDate = DateTime.MinValue,
                   DeadlineDate = t.DeadLine,
                   CompleteDate = t.Complete,
                   Deliverables = t.Deliverables,
                   Remarks = t.Remarks,
                   Engineer = new BO.EngineerInTask()
                   {
                       Id = _dal.Engineer!.Read(t.Id)!.Id,
                       Name = _dal.Engineer!.Read(t.Id)!.Name
                   },
                   ComplexityLevel = t.ComplexityLevel

               };
    }

    public void Update(BO.Task item)
    {
        if (_dal.Task.Read(item.Id) == null)
            throw new BO.BlDoesNotExistException($"task with ID={item.Id} does Not exist");
        try
        {
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
                ComplexityLevel = item.ComplexityLevel
            };
            _dal.Task!.Update(updatedTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"task with ID={item.Id} does Not exist",ex);

        }
    }
}
