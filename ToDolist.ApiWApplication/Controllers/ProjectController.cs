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
    public class ProjectController : ApiController
    {
        ProjectService services = new ProjectService();
        public ResponseModel Get()
        {
            
            ResponseModel responsemodel;
            try
            {
                List<ProjectViewModel> projects = services.GetAllProject();
                responsemodel = new ResponseModel(projects);
            }
            catch (Exception exception)
            {

                responsemodel = new ResponseModel(null, false, "Error Occure", exception);
            }


            return responsemodel;
        }

        public ResponseModel Get(int projectId)
        {
            Project projectDetails = services.GetProjectById(projectId);
            projectDetails = new Project(){ProjectId = projectDetails.ProjectId,ProjectName = projectDetails.ProjectName};
            return new ResponseModel(projectDetails);
        }
        public ResponseModel Post(Project project)
        {
            ResponseModel responsemodel;
            try
            {
                int projectId = services.SaveProject(project);
                responsemodel = projectId>0 ? new ResponseModel(project) : new ResponseModel(null,false,"Could't Save Project");
            }
            catch (Exception exception)
            {

                responsemodel = new ResponseModel(null, false, "Error Occure", exception);
            }


            return responsemodel;
            
        }

        public ResponseModel DeleteProject(int projectId)
        {
            ResponseModel responsemodel;
            try
            {
                bool deletedProject = services.DeleteProject(projectId);
                responsemodel = deletedProject ? new ResponseModel(projectId) : new ResponseModel(null, false, "Could't Deleted Project");
            }
            catch (Exception exception)
            {

                responsemodel = new ResponseModel(null, false, "Error Occure", exception);
            }


            return responsemodel;
        }
    }
}
