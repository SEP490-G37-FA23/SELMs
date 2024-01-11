using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository repository = new ProjectRepository();
        private IMemberProjectHistoryRepository projectMemberHistoryRepository = new MemberProjectHistoryRepository();
        private IEquipmentProjectHistoryRepository projectEquipmentHistoryRepository = new EquipmentProjectHistoryRepository();

        public async Task<ProjectModel> GetProject(int id)
        {
            try
            {
                dynamic project = repository.GetProject(id);
                List<User> projectMembers = await repository.GetProjectMembers(id);
                List<Equipment> projectEquipments = await repository.GetProjectEquipments(id);
                ProjectModel projectModel = new ProjectModel()
                {
                    Project = project,
                    ProjectEquipments = projectEquipments,
                    ProjectMembers = projectMembers
                };
                return projectModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveProject(Project project, List<string> projectMembers, List<string> projectEquipments)
        {
            try
            {
                project = repository.SaveProject(project);

                foreach (string user in projectMembers)
                {
                    Member_Project_History history = new Member_Project_History()
                    {
                        user_code = user,
                        project_id = project.project_id,
                        date = DateTime.Now,
                        status = "ACTIVE",
                        note = ""
                    };
                    await projectMemberHistoryRepository.SaveHistory(history);
                }

                foreach (string equipment in projectEquipments)
                {
                    Equipment_Project_History history = new Equipment_Project_History()
                    {
                        project_id = project.project_id,
                        system_equiment_code = equipment,
                        from_date = DateTime.Now,
                        to_date = project.end_date,
                        note = ""
                    };
                    await projectEquipmentHistoryRepository.SaveHistory(history);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task UpdateProject(int id, Project project, List<string> projectMembers, List<string> projectEquipments)
        {
            try
            {
                if (repository.GetProject(id) != null)
                {
                    project.project_id = id;
                    project = repository.UpdateProject(project);
                    List<User> currentMembers = await repository.GetProjectMembers(id);
                    List<string> currentMemberCodes = new List<string>();
                    foreach (User user in currentMembers)
                    {
                        currentMemberCodes.Add(user.user_code);
                    }
                    
                    List<Equipment> currentEquipments = await repository.GetProjectEquipments(id);
                    List<string> currentEquipmentCodes = new List<string>();

                    foreach (Equipment equip in currentEquipments)
                    {
                        currentEquipmentCodes.Add(equip.system_equipment_code);
                    }

                    foreach (string user in projectMembers)
                    {
                        if (!currentMemberCodes.Contains(user))
                        {
                            Member_Project_History history = new Member_Project_History()
                            {
                                user_code = user,
                                project_id = project.project_id,
                                date = DateTime.Now,
                                status = "ACTIVE",
                                note = ""
                            };
                            await projectMemberHistoryRepository.SaveHistory(history);
                        }
                    }

                    foreach (string equipment in projectEquipments)
                    {
                        if (!currentEquipmentCodes.Contains(equipment))
                        {
                            Equipment_Project_History history = new Equipment_Project_History()
                            {
                                project_id = project.project_id,
                                system_equiment_code = equipment,
                                from_date = DateTime.Now,
                                to_date = project.end_date,
                                note = ""
                            };
                            await projectEquipmentHistoryRepository.SaveHistory(history);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task CancelProject(int id)
        {
            try
            {
                Project project = repository.GetProject(id);
                project.end_date = DateTime.Now;
                project.status = false;
                repository.UpdateProject(project);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}