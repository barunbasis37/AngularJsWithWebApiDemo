
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
    public class TaskCompleteController : ApiController
    {

       public ResponseModel Post(Task task)
        {
            TaskService service=new TaskService();
            ResponseModel responseModel;
            try
            {
                bool complete = service.MarkCompleted(task);
                responseModel=new ResponseModel(isSuccess:complete); 
            }
            catch (Exception exception)
            {
                responseModel = new ResponseModel(isSuccess: false,exception:exception,message:"Couldn't Mark Completed The Task"); 
            }
            return responseModel;
        }
    }
}
