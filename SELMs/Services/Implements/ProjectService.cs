using SELMs.Models;
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