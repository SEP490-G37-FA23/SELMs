using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
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

        public dynamic GetLastDailyApplication()
         {
            Equipment_Handover_Form result =
                db.Equipment_Handover_Form.OrderByDescending(e => e.form_code).FirstOrDefault();
            return result;
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
               username = arg.username

            }
                , commandType: CommandType.StoredProcedure);
            return applications;
        }

        public dynamic UpdateApplication(Equipment_Handover_Form application)
        {
            Equipment_Handover_Form orgApplication = db.Equipment_Handover_Form
                .Where(p => p.form_id == application.form_id).FirstOrDefault();
            db.Entry(orgApplication).CurrentValues.SetValues(application);
            db.SaveChangesAsync();
            return orgApplication;
        }


        public dynamic GetApplicationDetailList(string applicationCode)
        {
            dynamic applicationDetails = db.Equipment_Handover_Form_Detail.
                Where(a => a.form_code.Equals(applicationCode)).ToListAsync();
            return applicationDetails;
        }

        public dynamic GetApplicationDetailList(Argument arg)
        {
            dynamic applicationDetails = null;
            applicationDetails = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEquipmentHandoverFormDetailList", new
            {
                //Add arguments here

            }
                , commandType: CommandType.StoredProcedure);
            return applicationDetails;
        }

        public dynamic GetApplicationDetail(int id)
        {
            dynamic applicationDetail = db.Equipment_Handover_Form_Detail
                .Where(a => a.application_detail_id == id).FirstOrDefault();
            return applicationDetail;
        }

        public dynamic SaveApplicationDetail(Equipment_Handover_Form_Detail applicationDetail)
        {
            db.Equipment_Handover_Form_Detail.Add(applicationDetail);
            db.SaveChanges();
            return applicationDetail;
        }

        public dynamic SaveApplicationDetails(List<Equipment_Handover_Form_Detail> applicationDetails)
        {
            db.Equipment_Handover_Form_Detail.AddRange(applicationDetails);
            db.SaveChanges();
            return applicationDetails;
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

        public dynamic GetApplicationAttachment(int applicationId)
        {
            var attachment = 
                (from a in db.Equipment_Handover_Form 
                join aa in db.Application_Attachment on a.form_id equals aa.application_id
                join at in db.Attachments on aa.attachment_id equals at.attach_id
                where aa.application_type.Equals(ApplicationType.EFH) && aa.application_id.Equals(applicationId)
                select at).FirstOrDefaultAsync();
            return attachment;
        }

        public dynamic AddAttachment(int applicationId, int attachmentId)
        {
            var applicationAttachment = new Application_Attachment()
            {
                application_id = applicationId,
                application_type = ApplicationType.EFH,
                attachment_id = attachmentId
            };
            db.Application_Attachment.Add(applicationAttachment);
            db.SaveChanges();
            return applicationAttachment;
        }
    }
}