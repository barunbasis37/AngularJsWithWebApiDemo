using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDolist.DataAccess;
using ToDolist.Service;
using ToDolist.ViewModel;

namespace ToDolist.ApiWApplication.Controllers
{
    public class TaskController : ApiController
    {

        TaskService services = new TaskService();
        public ResponseModel GetTask(int projectId, string sortOn="", string sortBy="")
        {

            ResponseModel responsemodel;
            try
            {
                List<TaskViewModel> Tasks = projectId == 0 ? services.GetAllTask() : services.GetAllTaskByProjectId(projectId, sortOn, sortBy);
                responsemodel = new ResponseModel(Tasks);
            }
            catch (Exception exception)
            {

                responsemodel = new ResponseModel(null, false, "Error Occure", exception);
            }


            return responsemodel;
        }

        public ResponseModel GetTaskById(int taskId)
        {
            ResponseModel responsemodel;
            try
            {
                if (taskId>0)
                {
                    Task task = services.GetTaskByTaskId(taskId);
                    responsemodel = new ResponseModel(task);
                }
                else
                {
                    responsemodel = new ResponseModel(isSuccess:false, message:"Id Can't Be Zero");
                }
                
                
            }
            catch (Exception exception)
            {

                responsemodel = new ResponseModel(null, false, "Error Occure", exception);
            }


            return responsemodel;
        }

        public ResponseModel Post(Task task)
        {
            ResponseModel responsemodel;
            try
            {
                task.Project = null;
                int taskId = services.SaveTask(task);
                responsemodel = taskId > 0 ? new ResponseModel(taskId) : new ResponseModel(null, false, "Could't Save Task");
            }
            catch (Exception exception)
            {

                responsemodel = new ResponseModel(null, false, "Error Occure", exception);
            }


            return responsemodel;

        }

        public ResponseModel DeleteTask(int taskId)
        {
            ResponseModel responsemodel;
            try
            {
                bool deletedTask = services.DeleteTask(taskId);
                responsemodel = deletedTask ? new ResponseModel(taskId) : new ResponseModel(null, false, "Could't Deleted Task");
            }
            catch (Exception exception)
            {

                responsemodel = new ResponseModel(null, false, "Error Occure", exception);
            }


            return responsemodel;
        }
    }
}
