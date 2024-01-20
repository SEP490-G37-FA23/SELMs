using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories.Implements
{
	public class ProjectRepository : IProjectRepository
	{
		private SELMsContext db = new SELMsContext();
		public void DeleteProject(int id)
		{
			try
			{
				Project project = db.Projects.Where(p => p.project_id == id).FirstOrDefault();

				if (project != null)
				{
					project.status = false;
					this.UpdateProject(project);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public dynamic GetProject(int id)
		{
            dynamic project = null;
            project = db.Database.Connection.QuerySingleAsync<dynamic>("Proc_GetDetailProject", new
            {
                project_id = id

            }
                , commandType: CommandType.StoredProcedure).Result;
            return project;
        }

		public dynamic GetProjectList()
		{
			dynamic projects = db.Projects.ToListAsync();
			return projects;
		}

		public dynamic SaveProject(Project project)
		{
			db.Projects.Add(project);
			db.SaveChanges();
			return project;
		}

		public dynamic GetProjectList(Argument arg)
		{
			dynamic projects = null;
			projects = db.Database.Connection.QueryAsync<dynamic>("Proc_GetAllProjectList", new
			{
				username = arg.username,
				project_name = arg.project_name,
				manager_name = arg.manager_name,
				location_name = arg.location_name
			}
				, commandType: CommandType.StoredProcedure);
			return projects;
		}
		public dynamic GetDoingProjectList(Argument arg)
		{
			dynamic projects = null;
			projects = db.Database.Connection.QueryAsync<dynamic>("Proc_GetProjectList", new
			{
				username = arg.username,
				project_name = arg.project_name
			}
				, commandType: CommandType.StoredProcedure);
			return projects;
		}

		public dynamic UpdateProject(Project project)
		{
			Project orgProject = db.Projects.Where(p => p.project_id == project.project_id).FirstOrDefault();
			db.Entry(orgProject).CurrentValues.SetValues(project);
			db.SaveChanges();
			return orgProject;
		}

		public dynamic GetProjectMembers(int id)
		{
			//List<User> projectMembers = db.Projects
			//    .Join(db.Member_Project_History, p => p.project_id, pu => pu.project_id, (u,pu) => new { u, pu })
			//    .Join(db.Users, ppu => ppu.pu.user_code, u => u.user_code, (ppu, u) => new { ppu, u })
			//    .Select( x => new { 
			//        user_code = x.user_code,

			//    }

			//    )

			var projectMembers = 
				(from u in db.Users
				 join pu in db.Member_Project_History on u.user_code equals pu.user_code
				 join p in db.Projects on pu.project_id equals p.project_id
				 where p.project_id.Equals(id) && u.resignation_date.Equals(null)
				 select u).ToList();
			return projectMembers;
		}

		public dynamic GetProjectEquipments(int id)
		{
			var projectEquipments =
				(from e in db.Equipments
				 join pe in db.Equipment_Project_History on e.system_equipment_code equals pe.system_equiment_code
				 join p in db.Projects on pe.project_id equals p.project_id
				 where p.project_id.Equals(id) && (pe.to_date == null || pe.to_date >= DateTime.Now)
				 select e).ToList();
			return projectEquipments;
		}

		public dynamic FindActiveMember(int projectId, string memberCode)
		{
			throw new NotImplementedException();
		}
	}
}