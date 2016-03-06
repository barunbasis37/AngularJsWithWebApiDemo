using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDolist.DataAccess;
using ToDolist.ViewModel;

namespace ToDolist.Service
{
    public class ProjectService
    {
        public List<ProjectViewModel> GetAllProject()
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            IQueryable<Project>dbset=dbModel.Projects.AsQueryable();
            List<ProjectViewModel> list = dbset.Select(p => new ProjectViewModel() { ProjectId = p.ProjectId, ProjectName = p.ProjectName, Count = p.Count }).ToList();
            return list;
        }

        public int SaveProject(Project project)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            Project projectadded;
            if (project.ProjectId>0)
            {
                projectadded=dbModel.Projects.Find(project.ProjectId);
                if (projectadded!=null)
                {
                    projectadded.ProjectName = project.ProjectName;
                    projectadded.Changed = DateTime.Now;
                }
            }
            else
            {
                project.Changed = DateTime.Now;
                project.Created = DateTime.Now;
                projectadded = dbModel.Projects.Add(project);
            }
            
            dbModel.SaveChanges();
            return projectadded.ProjectId;
        }

        public Project GetProjectById(int projectId)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            Project projectDetails = dbModel.Projects.Find(projectId);
            return projectDetails;
        }

        public bool DeleteProject(int projectId)
        {
            ToDoListEntities dbModel = new ToDoListEntities();
            Project projectFind = dbModel.Projects.Find(projectId);
            if (projectFind != null)
            {
                dbModel.Projects.Remove(projectFind);
                dbModel.SaveChanges();
            }
            return true;
        }
    }
}
