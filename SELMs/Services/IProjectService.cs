using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface IProjectService
    {
        Task<ProjectModel> GetProject(int id);
        Task SaveProject(Project project, List<User> projectMembers, List<Equipment> projectEquipments);
        Task UpdateProject(int id, Project project, List<User> projectMembers, List<Equipment> projectEquipments);
        Task CancelProject(int id);
    }
}
