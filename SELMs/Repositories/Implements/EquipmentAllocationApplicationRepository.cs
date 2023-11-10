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
    public class EquipmentAllocationApplicationRepository : IEquipmentAllocationApplicationRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteApplication(int id)
        {
            dynamic application = db.Equipment_Allocation_Application.Where(a => a.application_id == id).FirstOrDefault();
            if (application != null) db.Equipment_Allocation_Application.Remove(application);
            db.SaveChangesAsync();
        }

        public dynamic GetApplication(int id)
        {
            dynamic application = db.Equipment_Allocation_Application.Where(a => a.application_id == id).FirstOrDefault();
            return application;
        }

        public dynamic GetApplicationList()
        {
            dynamic applications = db.Equipment_Allocation_Application.ToListAsync();
            return applications;
        }

        public dynamic GetLastDailyApplication()
        {
            dynamic application = db.Equipment_Import_Application
                .OrderBy(a => a.application_date).Reverse().FirstOrDefault();
            return application;
        }

        public dynamic SaveApplication(Equipment_Allocation_Application application)
        {
            db.Equipment_Allocation_Application.Add(application);
            db.SaveChanges();
            return application;
        }

        public dynamic GetApplicationList(Argument arg)
        {
            dynamic applications = null;
            applications = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEquipmentAllocationApplicationList", new
            {
                //Add arguments here
                
            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }

        public void UpdateApplication(Equipment_Allocation_Application application)
        {
            Equipment_Allocation_Application orgApplication = db.Equipment_Allocation_Application
                .Where(p => p.application_id == application.application_id).FirstOrDefault();
            db.Entry(orgApplication).CurrentValues.SetValues(application);
            db.SaveChangesAsync();
        }

        
        public dynamic GetApplicationDetailList(string applicationCode)
        {
            dynamic applicationDetails = db.Equipment_Allocation_Application_Detail
                .Where(a => a.ea_application_code == applicationCode).ToListAsync();
            return applicationDetails;
        }

        public dynamic GetApplicationDetailList(Argument arg)
        {
            dynamic applications = null;
            applications = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEquipmentAllocationApplicationDetailList", new
            {
                //Add arguments here

            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }

        public dynamic GetApplicationDetail(int id)
        {
            dynamic application = db.Equipment_Allocation_Application_Detail
                .Where(a => a.application_detail_id == id).FirstOrDefault();
            return application;
        }

        public dynamic SaveApplicationDetail(Equipment_Allocation_Application_Detail application)
        {
            db.Equipment_Allocation_Application_Detail.Add(application);
            db.SaveChanges();
            return application;
        }

        public dynamic SaveApplicationDetails(List<Equipment_Allocation_Application_Detail> applications)
        {
            db.Equipment_Allocation_Application_Detail.AddRange(applications);
            db.SaveChanges();
            return applications;
        }

        public void UpdateApplicationDetail(Equipment_Allocation_Application_Detail applicationDetail)
        {
            Equipment_Allocation_Application_Detail orgApplicationDetail = db.Equipment_Allocation_Application_Detail
                .Where(p => p.application_detail_id == applicationDetail.application_detail_id).FirstOrDefault();
            db.Entry(orgApplicationDetail).CurrentValues.SetValues(applicationDetail);
            db.SaveChangesAsync();
        }

        public void DeleteApplicationDetail(int id)
        {
            dynamic applicationDetail = db.Equipment_Allocation_Application_Detail
                .Where(a => a.application_detail_id == id).FirstOrDefault();
            if (applicationDetail != null) db.Equipment_Allocation_Application_Detail.Remove(applicationDetail);
            db.SaveChangesAsync();
        }
    }
}