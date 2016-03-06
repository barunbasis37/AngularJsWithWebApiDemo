using System;
using System.Collections.Generic;
using System.Linq;
using ToDolist.DataAccess;
using ToDolist.ViewModel;

namespace ToDolist.Service
{
    public class TaskService
    {
        public List<TaskViewModel> GetAllTask()
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            IQueryable<Task> dbset = dbModel.Tasks.AsQueryable();
            List<TaskViewModel> list =
                dbset.Select(
                    p =>
                        new TaskViewModel()
                        {
                            TaskId = p.TaskId,
                            TaskName = p.TaskName,
                            ProjectId = p.ProjectRefId,
                            ProjectName = p.Project.ProjectName,
                            Priority = p.Priority,
                            DueDate = p.DueDate,
                            IsCompleted = p.IsCompleted
                        }).ToList();
            return list;
        }

        

        public int SaveTask(Task task)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            Task taskadded;
            if (task.TaskId > 0)
            {
                taskadded = dbModel.Tasks.Find(task.TaskId);
                if (taskadded != null)
                {
                    taskadded.TaskName = task.TaskName;
                    taskadded.DueDate = task.DueDate;
                    taskadded.Priority = task.Priority;
                    taskadded.IsCompleted = task.IsCompleted;
                    taskadded.ProjectRefId = task.ProjectRefId;
                    taskadded.Changed = DateTime.Now;
                }
            }
            else
            {
                task.Changed = DateTime.Now;
                task.Created = DateTime.Now;
                taskadded = dbModel.Tasks.Add(task);

            }
            dbModel.SaveChanges();
            UpdateProjectCount(task.ProjectRefId, dbModel);
            return taskadded.TaskId;
        }

        //try
        //{

        //}
        //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
        //{
        //    Exception raise = dbEx;
        //    foreach (var validationErrors in dbEx.EntityValidationErrors)
        //    {
        //        foreach (var validationError in validationErrors.ValidationErrors)
        //        {
        //            string message = string.Format("{0}:{1}",
        //                validationErrors.Entry.Entity.ToString(),
        //                validationError.ErrorMessage);
        //            // raise a new exception nesting
        //            // the current instance as InnerException
        //            raise = new InvalidOperationException(message, raise);
        //        }
        //    }
        //    throw raise;
        //}

        public List<TaskViewModel> GetAllTaskByProjectId(int projectId)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            IQueryable<Task> dbset = dbModel.Tasks.Where(t => t.ProjectRefId == projectId).AsQueryable();
            List<TaskViewModel> list =
                dbset.Select(
                    p =>
                        new TaskViewModel()
                        {
                            TaskId = p.TaskId,
                            TaskName = p.TaskName,
                            ProjectId = p.ProjectRefId,
                            ProjectName = p.Project.ProjectName,
                            Priority = p.Priority,
                            IsCompleted = p.IsCompleted,
                            DueDate = p.DueDate
                        }).ToList();
            return list;

        }

        public bool DeleteTask(int taskId)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            Task taskFind = dbModel.Tasks.Find(taskId);
            if (taskFind != null)
            {
                dbModel.Tasks.Remove(taskFind);
                dbModel.SaveChanges();
            }
            return true;
        }

        public Task GetTaskByTaskId(int taskId)
        {
           ToDoListEntities dbModel = new ToDoListEntities();
            Task taskFind = dbModel.Tasks.Find(taskId);
            return new Task()
            {
                ProjectRefId = taskFind.ProjectRefId,
                TaskName = taskFind.TaskName,
                DueDate = taskFind.DueDate,
                IsCompleted = taskFind.IsCompleted,
                Priority = taskFind.Priority,
            };
        }

        public bool MarkCompleted(Task task)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            var taskFindDb = dbModel.Tasks.Find(task.TaskId);
            if (taskFindDb!=null)
            {
                taskFindDb.IsCompleted = true;
                dbModel.SaveChanges();
                int projectId = taskFindDb.ProjectRefId;
                UpdateProjectCount(projectId,dbModel);
            }
            return true;
        }

        private static void UpdateProjectCount(int projectId,ToDoListEntities dbModel)
        {

            int unFinishedTask = dbModel.Tasks.Count(tCount => tCount.ProjectRefId == projectId && tCount.IsCompleted == false);
            Project dbProject = dbModel.Projects.Find(projectId);
            dbProject.Count = unFinishedTask;
            dbModel.SaveChanges();            
        }

        public List<TaskViewModel> GetAllTaskByProjectId(int projectId, string sortOn, string sortBy)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            IQueryable<Task> queryable = dbModel.Tasks.Where(t => t.ProjectRefId == projectId).AsQueryable();

            if (sortBy=="desc")
            {
                queryable = sortOn=="date" ? queryable.OrderByDescending(d => d.DueDate) : queryable.OrderByDescending(d => d.IsCompleted);
            }
            else
            {
                queryable = sortOn == "date" ? queryable.OrderBy(d => d.DueDate) : queryable.OrderBy(d => d.IsCompleted);
            }

            List<TaskViewModel> taskViewModels =
                queryable.Select(
                    p =>
                        new TaskViewModel()
                        {
                            TaskId = p.TaskId,
                            TaskName = p.TaskName,
                            ProjectId = p.ProjectRefId,
                            ProjectName = p.Project.ProjectName,
                            Priority = p.Priority,
                            IsCompleted = p.IsCompleted,
                            DueDate = p.DueDate
                        }).ToList();
            return taskViewModels;
        }
    }
}