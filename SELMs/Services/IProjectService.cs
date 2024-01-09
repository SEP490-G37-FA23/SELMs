using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Services
{
	public interface IProjectService
	{
		Task<ProjectDetailModel> GetProject(int id);
		Task SaveProject(Project project, List<string> projectMembers, List<string> projectEquipments);
		Task UpdateProject(int id, Project project, List<string> projectMembers, List<string> projectEquipments);
		Task CancelProject(int id);
	}
}
