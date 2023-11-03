using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class ProjectRepository : IProjectRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteProject(int id)
        {
            dynamic project = db.Projects.Where(p => p.project_id == id).FirstOrDefault();
            if (project != null) db.Projects.Remove(project);
            db.SaveChangesAsync();
        }

        public dynamic GetProject(int id)
        {
            dynamic project = db.Projects.Where(p => p.project_id == id).FirstOrDefault();
            return project;
        }

        public dynamic GetProjectList()
        {
            dynamic categories = db.Projects.ToListAsync();
            return categories;
        }

        public dynamic SaveProject(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
            return project;
        }

        public dynamic GetProjectList(Argument arg)
        {
            dynamic categories = null;
            categories = db.Database.Connection.QueryAsync<dynamic>("Proc_GetProjectList", new
            {
                username = arg.username,
            }
                , commandType: CommandType.StoredProcedure);
            return categories;
        }

        public void UpdateProject(Project project)
        {
            Project orgProject = db.Projects.Where(p => p.project_id == project.project_id).FirstOrDefault();
            db.Entry(orgProject).CurrentValues.SetValues(project);
            db.SaveChangesAsync();
        }
    }
}