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
    public class EquipmentHandoverFormRepository : IEquipmentHandoverFormRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteApplication(int id)
        {
            dynamic application = db.Equipment_Handover_Form.Where(a => a.form_id == id).FirstOrDefault();
            if (application != null) db.Equipment_Handover_Form.Remove(application);
            db.SaveChangesAsync();
        }

        public dynamic GetApplication(int id)
        {
            dynamic application = db.Equipment_Handover_Form.Where(a => a.form_id == id).FirstOrDefault();
            return application;
        }

        public dynamic GetApplicationList()
        {
            dynamic applications = db.Equipment_Handover_Form.ToListAsync();
            return applications;
        }

        public dynamic SaveApplication(Equipment_Handover_Form application)
        {
            db.Equipment_Handover_Form.Add(application);
            db.SaveChanges();
            return application;
        }

        public dynamic GetApplicationList(Argument arg)
        {
            dynamic applications = null;
            applications = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEquipmentHandoverFormList", new
            {
                //Add arguments here

            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }

        public void UpdateApplication(Equipment_Handover_Form application)
        {
            Equipment_Handover_Form orgApplication = db.Equipment_Handover_Form
                .Where(p => p.form_id == application.form_id).FirstOrDefault();
            db.Entry(orgApplication).CurrentValues.SetValues(application);
            db.SaveChangesAsync();
        }


        public dynamic GetApplicationDetailList()
        {
            dynamic applicationDetails = db.Equipment_Handover_Form_Detail.ToListAsync();
            return applicationDetails;
        }

        public dynamic GetApplicationDetailList(Argument arg)
        {
            dynamic applications = null;
            applications = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEquipmentHandoverFormDetailList", new
            {
                //Add arguments here

            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }

        public dynamic GetApplicationDetail(int id)
        {
            dynamic application = db.Equipment_Handover_Form_Detail
                .Where(a => a.application_detail_id == id).FirstOrDefault();
            return application;
        }

        public dynamic SaveApplicationDetail(Equipment_Handover_Form_Detail application)
        {
            db.Equipment_Handover_Form_Detail.Add(application);
            db.SaveChanges();
            return application;
        }

        public dynamic SaveApplicationDetails(List<Equipment_Handover_Form_Detail> applications)
        {
            db.Equipment_Handover_Form_Detail.AddRange(applications);
            db.SaveChanges();
            return applications;
        }

        public void UpdateApplicationDetail(Equipment_Handover_Form_Detail applicationDetail)
        {
            Equipment_Handover_Form_Detail orgApplicationDetail = db.Equipment_Handover_Form_Detail
                .Where(p => p.application_detail_id == applicationDetail.application_detail_id).FirstOrDefault();
            db.Entry(orgApplicationDetail).CurrentValues.SetValues(applicationDetail);
            db.SaveChangesAsync();
        }

        public void DeleteApplicationDetail(int id)
        {
            dynamic applicationDetail = db.Equipment_Handover_Form_Detail
                .Where(a => a.application_detail_id == id).FirstOrDefault();
            if (applicationDetail != null) db.Equipment_Handover_Form_Detail.Remove(applicationDetail);
            db.SaveChangesAsync();
        }
    }
}