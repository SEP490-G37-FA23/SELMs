﻿using Dapper;
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

        public dynamic GetApplicationByCode(string applicationCode)
        {
            dynamic application = db.Equipment_Allocation_Application.Where(a => a.ea_application_code == applicationCode).FirstOrDefault();
            return application;
        }

        public dynamic GetApplicationList()
        {
            dynamic applications = db.Equipment_Allocation_Application.ToListAsync();
            return applications;
        }

        public dynamic GetLastDailyApplication()
        {
            Equipment_Allocation_Application result = db.Equipment_Allocation_Application.OrderByDescending(e => e.ea_application_code).FirstOrDefault();
            return result;
        }

        public dynamic SaveApplication(Equipment_Allocation_Application application)
        {
            try
            {
                db.Equipment_Allocation_Application.Add(application);
                db.SaveChanges();
                return application;
            } catch(Exception ex)
            {
                return ex.Message;
            }
           
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

        public dynamic UpdateApplication(Equipment_Allocation_Application application)
        {
            Equipment_Allocation_Application orgApplication = db.Equipment_Allocation_Application
                .Where(p => p.application_id == application.application_id).FirstOrDefault();
            db.Entry(orgApplication).CurrentValues.SetValues(application);
            db.SaveChanges();
            return orgApplication;
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

        public dynamic GetEAAList(Argument arg)
        {
            dynamic list = null;
            list = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEAAList", new
            {
               username = arg.username,
               project_name = arg.text,
               location_name = arg.text1,
               creater_name = arg.text2,
               application_code=arg.text3
            }
                , commandType: CommandType.StoredProcedure);
            return list;
        }

        public dynamic GetAvailableEquipmentList(Argument arg)
        {
            dynamic list = null;
            list = db.Database.Connection.QueryAsync<dynamic>("Proc_GetAvailableEAAList", new
            {
                username = arg.username,
                project_name = arg.text,
                location_name = arg.text1,
                creater_name = arg.text2,
                application_code = arg.text3
            }
                , commandType: CommandType.StoredProcedure);
            return list;
        }

        public dynamic GetNeedImportEquipmentList(Argument arg)
        {
            dynamic list = null;
            list = db.Database.Connection.QueryAsync<dynamic>("Proc_GetNeedImportEAAList", new
            {
                username = arg.username,
                project_name = arg.text,
                location_name = arg.text1,
                creater_name = arg.text2,
                application_code = arg.text3
            }
                , commandType: CommandType.StoredProcedure);
            return list;
        }
    }
}