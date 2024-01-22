using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IProjectRepository
    {
        dynamic GetProjectList();
        dynamic GetProjectList(Argument arg);
        dynamic GetDoingProjectList(Argument arg);
        dynamic GetProject(int id);
        dynamic SaveProject(Project project);
        dynamic UpdateProject(Project project);
        void DeleteProject(int id);
        dynamic GetProjectMembers(int id);
        void RemoveProjectMember(int projectId, string userCode);
        dynamic GetProjectEquipments(int id);

        dynamic FindActiveMember(int projectId, string memberCode);
        
    }
}
