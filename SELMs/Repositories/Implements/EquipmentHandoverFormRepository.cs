using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories.Implements
{
	public class EquipmentHandoverFormRepository : IEquipmentHandoverFormRepository
	{
		private SELMsContext db = new SELMsContext();
		public void DeleteApplication(int id)
		{
			Equipment_Handover_Form application = db.Equipment_Handover_Form.Where(a => a.form_id == id).FirstOrDefault();
			if (application != null)
			{
				List<Equipment_Handover_Form_Detail> applicationDetails = GetApplicationDetailList(application.form_code);
				if (applicationDetails != null)
					db.Equipment_Handover_Form_Detail.RemoveRange(applicationDetails);
				db.Equipment_Handover_Form.Remove(application);
			}
			db.SaveChanges();
		}

		public dynamic GetApplicationDetailById(int id)
		{
			dynamic application = null;
			application = db.Database.Connection.QuerySingleAsync<dynamic>("Proc_GetDetailHEF", new
			{
				form_id = id

			}
				, commandType: CommandType.StoredProcedure).Result;
			return application;
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
				Where(a => a.form_code.Equals(applicationCode)).ToList();
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
			foreach (var detail in applicationDetails)
			{
				Equipment_Allocation_Application_Detail allocation = db.Equipment_Allocation_Application_Detail
								.Where(p => p.application_detail_id == detail.application_detail_id).FirstOrDefault();
				allocation.status = "Đã bàn giao";
			}
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
			dynamic attachment =
				(from at in db.Attachments
				 join aa in db.Application_Attachment on at.attach_id equals aa.attachment_id
				 join a in db.Equipment_Handover_Form on aa.application_id equals a.form_id
				 where aa.application_type == ApplicationType.EHF && aa.application_id == applicationId
				 select at).ToList();
			return attachment;
		}

		public dynamic AddAttachment(int applicationId, int attachmentId)
		{
			var applicationAttachment = new Application_Attachment()
			{
				application_id = applicationId,
				application_type = ApplicationType.EHF,
				attachment_id = attachmentId
			};
			db.Application_Attachment.Add(applicationAttachment);
			db.SaveChanges();
			return applicationAttachment;
		}

		public void DeleteAttachment(int applicationId, int attachmentId)
		{
			Application_Attachment appAttach = db.Application_Attachment
				.Where(aa => aa.application_id == applicationId && aa.attachment_id == attachmentId && aa.application_type == ApplicationType.EHF)
				.FirstOrDefault();
			Attachment attachment = db.Attachments.Where(a => a.attach_id == attachmentId).FirstOrDefault();
			db.Application_Attachment.Remove(appAttach);
			db.Attachments.Remove(attachment);
		}

		public async Task<Equipment_Handover_Form> GetFormDetail(string formCode)
		{
			return await db.Equipment_Handover_Form
				.Include(a => a.User)
				.Include(a => a.Project)
				.Include(a => a.Location)
				.FirstOrDefaultAsync(a => a.form_code.Equals(formCode));
		}
	}
}