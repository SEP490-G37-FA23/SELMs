﻿using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace SELMs.Repositories.Implements
{
    public class InventoryRequestApplicationRepository : IInventoryRequestApplicationRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteApplication(int id)
        {
            dynamic application = db.Inventory_Request_Application.Where(a => a.application_id == id).FirstOrDefault();
            if (application != null)
            {
                List<Inventory_Request_Application_Detail> applicationDetails = db.Inventory_Request_Application_Detail
                .Where(a => a.application_detail_id == id).ToList();
                db.Inventory_Request_Application_Detail.RemoveRange(applicationDetails);
                db.Inventory_Request_Application.Remove(application);
            }
            db.SaveChangesAsync();
        }

        public dynamic GetApplication(int id)
        {
            dynamic application = db.Inventory_Request_Application.Where(a => a.application_id == id).FirstOrDefault();
            return application;
        }

        public dynamic GetLastDailyApplication()
        {
            Inventory_Request_Application result =
                db.Inventory_Request_Application.OrderByDescending(e => e.ir_application_code).FirstOrDefault();
            return result;
        }

        public dynamic GetApplicationList()
        {
            dynamic applications = db.Inventory_Request_Application.ToListAsync();
            return applications;
        }

        public dynamic SaveApplication(Inventory_Request_Application application)
        {
            db.Inventory_Request_Application.Add(application);
            db.SaveChanges();
            return application;
        }

        public dynamic GetApplicationList(Argument arg)
        {
            dynamic applications = null;
            applications = db.Database.Connection.QueryAsync<dynamic>("Proc_GetInventoryRequestApplicationList", new
            {
                username = arg.username

            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }

        public dynamic UpdateApplication(Inventory_Request_Application application)
        {
            Inventory_Request_Application orgApplication = db.Inventory_Request_Application
                .Where(p => p.application_id == application.application_id).FirstOrDefault();
            db.Entry(orgApplication).CurrentValues.SetValues(application);
            db.SaveChangesAsync();
            return orgApplication;
        }


        public dynamic GetApplicationDetailList(string applicationCode)
        {
            dynamic applicationDetails = db.Inventory_Request_Application_Detail.
                Where(a => a.ir_application_code.Equals(applicationCode)).ToListAsync();
            return applicationDetails;
        }

        public dynamic GetApplicationDetailList(Argument arg)
        {
            dynamic applicationDetails = null;
            applicationDetails = db.Database.Connection.QueryAsync<dynamic>("Proc_GetInventoryRequestApplicationDetailList", new
            {
                //Add arguments here

            }
                , commandType: CommandType.StoredProcedure);
            return applicationDetails;
        }

        public async Task<Inventory_Request_Application_Detail> GetApplicationDetail(int id)
        {
            return  db.Inventory_Request_Application_Detail
                .Where(a => a.application_detail_id == id).FirstOrDefault();
        }

        public dynamic SaveApplicationDetail(Inventory_Request_Application_Detail applicationDetail)
        {
            db.Inventory_Request_Application_Detail.Add(applicationDetail);
            db.SaveChanges();
            return applicationDetail;
        }

        public dynamic SaveApplicationDetails(List<Inventory_Request_Application_Detail> applicationDetails)
        {
            foreach (var detail in applicationDetails)
            {
                Equipment_Allocation_Application_Detail allocation = db.Equipment_Allocation_Application_Detail
                                .Where(p => p.application_detail_id == detail.application_detail_id).FirstOrDefault();
                allocation.status = "Đã bàn giao";
            }
            db.Inventory_Request_Application_Detail.AddRange(applicationDetails);

            db.SaveChanges();
            return applicationDetails;
        }

        public dynamic UpdateApplicationDetail(Inventory_Request_Application_Detail applicationDetail)
        {
            // Retrieve the original application detail
            Inventory_Request_Application_Detail orgApplicationDetail = db.Inventory_Request_Application_Detail
                .Where(p => p.application_detail_id == applicationDetail.application_detail_id)
                .FirstOrDefault();

            // Update the values of the original application detail with the new values
            if (orgApplicationDetail != null)
            {
                db.Entry(orgApplicationDetail).CurrentValues.SetValues(applicationDetail);

                // Save changes
                db.SaveChanges();
            }
            return orgApplicationDetail;
        }


        public void DeleteApplicationDetail(int id)
        {
            dynamic applicationDetail = db.Inventory_Request_Application_Detail
                .Where(a => a.application_detail_id == id).FirstOrDefault();
            if (applicationDetail != null) db.Inventory_Request_Application_Detail.Remove(applicationDetail);
            db.SaveChanges();
        }

        public void DeleteAllApplicationDetails()
        {
            db.Inventory_Request_Application_Detail.RemoveRange(db.Inventory_Request_Application_Detail.ToList());
            db.SaveChanges();
        }

        public dynamic GetDetailIRAListInLocation(int location_id,Argument arg)
        {
            dynamic applications = null;
            applications = db.Database.Connection.QueryAsync<dynamic>("Proc_GetDetailIRAListInLocation", new
            {
                location_id = location_id,
                username= arg.username
            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }

        public dynamic GetResultIRAList(Argument arg)
        {
            dynamic applications = null;
            applications = db.Database.Connection.QueryAsync<dynamic>("Proc_GetResultIRAList", new
            {
                username = arg.username
            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }
    }
}