using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface IProjectService
    {
        Task SaveProject(Project project);
        Task UpdateProject(int id, Project project);
    }
}
