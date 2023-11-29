using SELMs.Models.BusinessModel;
using SELMs.Models;
using SELMs.Repositories.Implements;
using SELMs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace SELMs.Services.Implements
{
    public class EquipmentHandoverService : IEquipmentHandoverService
    {
        private IEquipmentHandoverFormRepository repository = new EquipmentHandoverFormRepository();
        private IAttachmentRepository attachmentRepository = new AttachmentRepository();
        public async Task<ApplicationModel> GetApplication(int id)
        {
            Equipment_Handover_Form application = repository.GetApplication(id);
            List<Equipment_Handover_Form> applicationDetails = repository.GetApplicationDetailList(application.form_code);
            Attachment attachment = repository.GetApplicationAttachment(application.form_id);
            ApplicationModel result = new ApplicationModel()
            {
                application = application,
                applicationDetails = applicationDetails,
                attachment = attachment
            };
            return result;
        }

        public async Task SaveApplication(Equipment_Handover_Form application, List<Equipment_Handover_Form_Detail> applicationDetails, HttpPostedFileBase file)
        {
            string applicationCode = GenerateApplicationCode();
            application.form_code = applicationCode;
            application.create_date = DateTime.Now;
            application.is_finish = false;
            Attachment attachment = new Attachment();
            foreach (Equipment_Handover_Form_Detail item in applicationDetails)
            {
                item.form_code = applicationCode;
            }

            application = repository.SaveApplication(application);
            repository.SaveApplicationDetails(applicationDetails);

            if (file != null)
            {
                attachment = CreateAttachment(file);
                attachment.name = applicationCode;
                attachment.date = DateTime.Now;
                attachment = attachmentRepository.SaveAttachment(attachment);
                repository.AddAttachment(application.form_id, attachment.attach_id);
            }
        }

        public async Task UpdateApplication(int id, Equipment_Handover_Form application, List<Equipment_Handover_Form_Detail> applicationDetails, HttpPostedFileBase file)
        {

            if (repository.GetApplication(id) != null)
            {
                application.form_id = id;
                application = repository.UpdateApplication(application);
            }
            foreach (Equipment_Handover_Form_Detail item in applicationDetails)
            {
                if (item.application_detail_id != null && repository.GetApplicationDetail(item.form_detail_id) != null)
                {
                    repository.UpdateApplicationDetail(item);
                }
                else
                {
                    item.form_code = application.form_code;
                    repository.SaveApplicationDetail(item);
                }
            }

        }

        public async Task<dynamic> AddAttachment(int id, HttpPostedFileBase file)
        {
            Equipment_Handover_Form application = repository.GetApplication(id);
            if (application != null)
            {
                Attachment attachment = CreateAttachment(file);
                attachment.name = application.form_code;
                attachment.date = DateTime.Now;
                attachment = attachmentRepository.SaveAttachment(attachment);
                repository.AddAttachment(application.form_id, attachment.attach_id);
                return attachment;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteAttachment(int applicationId, int attachmentId)
        {
            Equipment_Handover_Form application = repository.GetApplication(applicationId);
            Attachment attachment = attachmentRepository.GetAttachment(attachmentId);
            if (application != null && attachment != null)
            {
                repository.DeleteAttachment(applicationId, attachmentId);
            }
        }

        string GenerateApplicationCode()
        {
            string code = $"EHF{DateTime.Now.ToString("yyyyMMdd")}";

            Equipment_Handover_Form lastDailyApplication = repository.GetLastDailyApplication();

            int num = 1;

            if (lastDailyApplication != null)
            {
                string formCode = lastDailyApplication.form_code;

                if (formCode.StartsWith(code))
                {
                    string numericPart = formCode.Substring(code.Length);
                    num = int.TryParse(numericPart, out int parsedNum) ? parsedNum + 1 : 1;
                }
            }

            code += num < 10000 ? num.ToString("D4") : num.ToString();

            return code;
        }

        Attachment CreateAttachment(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            Attachment result = new Attachment()
            {
                name = file.FileName,
                content = reader.ReadBytes(file.ContentLength),
                extension = Path.GetExtension(file.FileName)
            };
            return result;
        }
    }
}