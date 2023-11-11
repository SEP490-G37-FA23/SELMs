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
        private IMemberProjectHistoryRepository projectMemberHistoryRepository = new MemberProjectHistoryRepository();
        private IEquipmentProjectHistoryRepository equipmentProjectHistoryRepository = new EquipmentProjectHistoryRepository();

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

        public async Task SaveProject(Project project, List<User> projectMembers, List<Equipment> projectEquipments)
        {
            project = repository.SaveProject(project);

            foreach (User user in projectMembers)
            {
                Member_Project_History history = new Member_Project_History()
                {
                    user_code = user.user_code,
                    project_id = project.project_id,
                    date = DateTime.Now,
                    status = "ACTIVE",
                    note = ""
                };
                projectMemberHistoryRepository.SaveHistory(history);
            }

			foreach (Equipment equipment in projectEquipments)
			{
				Equipment_Project_History history = new Equipment_Project_History()
				{
					project_id = project.project_id,
					system_equiment_code = equipment.system_equipment_code,
					from_date = DateTime.Now,
					to_date = project.end_date,
					note = ""
				};
				await projectEquipmentHistoryRepository.SaveHistory(history);
			}
		}

		public async Task UpdateProject(int id, Project project, List<User> projectMembers, List<Equipment> projectEquipments)
		{
			if (await repository.GetProject(id) != null)
			{
				project.project_id = id;
				project = repository.UpdateProject(project);
				List<User> currentMembers = repository.GetProjectMembers(id);
				List<Equipment> currentEquipments = repository.GetProjectEquipments(id);

				foreach (User user in projectMembers)
				{
				    if (!currentMembers.Contains(user))
				    {
				        Member_Project_History history = new Member_Project_History()
				        {
				            user_code = user.user_code,
				            project_id = project.project_id,
				            date = DateTime.Now,
				            status = "ACTIVE",
				            note = ""
				        };
				        projectMemberHistoryRepository.SaveHistory(history);
				    }
				}

                foreach (Equipment equipment in projectEquipments)
                {
                    if (!currentEquipments.Contains(equipment))
                    {
                        Equipment_Project_History history = new Equipment_Project_History()
                        {
                            project_id = project.project_id,
                            system_equiment_code = equipment.system_equipment_code,
                            from_date = DateTime.Now,
                            to_date = project.end_date,
                            note = ""
                        };
                        projectEquipmentHistoryRepository.SaveHistory(history);
                    }
                }
            }
		}

        public async Task CancelProject(int id)
        {
            Project project = repository.GetProject(id);
            project.end_date = DateTime.Now;
            project.status = false;
            repository.UpdateProject(project);
        }
    }
}