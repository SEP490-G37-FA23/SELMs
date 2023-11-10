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
        dynamic GetProject(int id);
        dynamic SaveProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int id);
        dynamic GetProjectMembers(int id);
        dynamic GetProjectEquipments(int id);
    }
}
