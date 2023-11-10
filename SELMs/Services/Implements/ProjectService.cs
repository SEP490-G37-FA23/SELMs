using SELMs.Models;
using SELMs.Models.BusinessModel;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services.Implements
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository repository = new ProjectRepository();

        public async Task<ProjectModel> GetProject(int id)
        {
            Project project = repository.GetProject(id);
            List<User> projectMembers = repository.GetProjectMembers(id);
            List<Equipment> projectEquipments = repository.GetProjectEquipments(id);
            ProjectModel projectModel = new ProjectModel()
            {
                Project = project,
                ProjectEquipments = projectEquipments,
                ProjectMembers = projectMembers
            };
            return projectModel;
        }

        public async Task SaveProject(Project project)
        {
            Project eq = project;
            repository.SaveProject(eq);
        }

        public async Task UpdateProject(int id, Project project)
        {
            if (await repository.GetProject(id) != null)
            {
                project.project_id = id;
                repository.UpdateProject(project);
            }
        }
    }
}