using SELMs.Models.BusinessModel;
using SELMs.Models;
using SELMs.Repositories.Implements;
using SELMs.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services.Implements
{
    public class InventoryRequestService : IInventoryRequestService
    {
        private IInventoryRequestApplicationRepository repository = new InventoryRequestApplicationRepository();
        private IAttachmentRepository attachmentRepository = new AttachmentRepository();
        public async Task<ApplicationModel> GetApplication(int id)
        {
            Inventory_Request_Application application = repository.GetApplication(id);
            List<Inventory_Request_Application> applicationDetails = repository.GetApplicationDetailList(application.ir_application_code);
            //Attachment attachment = repository.GetApplicationAttachment(application.application_id);
            ApplicationModel result = new ApplicationModel()
            {
                application = application,
                applicationDetails = applicationDetails,
                //attachment = attachment
            };
            return result;
        }

        public async Task<dynamic> SaveApplication(Inventory_Request_Application application, List<Inventory_Request_Application_Detail> applicationDetails)
        {
            string applicationCode = GenerateApplicationCode();
            application.ir_application_code = applicationCode;
            application.request_date = DateTime.Now.ToString();
            application.status = false;
            Attachment attachment = new Attachment();
            foreach (Inventory_Request_Application_Detail item in applicationDetails)
            {
                item.ir_application_code = applicationCode;
            }

            application = repository.SaveApplication(application);
            repository.SaveApplicationDetails(applicationDetails);
            return application;
        }

        public async Task<dynamic> UpdateApplication(int id, Inventory_Request_Application application, List<Inventory_Request_Application_Detail> applicationDetails)
        {

            if (repository.GetApplication(id) != null)
            {
                application.application_id = id;
                application = repository.UpdateApplication(application);
            }
            foreach (Inventory_Request_Application_Detail item in applicationDetails)
            {
                if (item.application_detail_id != null && repository.GetApplicationDetail(item.application_detail_id) != null)
                {
                    repository.UpdateApplicationDetail(item);
                }
                else
                {
                    item.ir_application_code = application.ir_application_code;
                    repository.SaveApplicationDetail(item);
                }
            }
            return application;
        }

        public async Task<dynamic> ConfirmApplication(int id, User member)
        {
            Inventory_Request_Application application = repository.GetApplication(id);
            if (application != null)
            {
                application.status = true;
                application.performer = member.user_code;
                repository.UpdateApplication(application);
                return application;
            }
            return null;
        }

        public async Task CancelApplication(int id)
        {
            Inventory_Request_Application application = repository.GetApplication(id);
            if (application != null)
            {
                repository.DeleteApplication(id);
            }
        }

        string GenerateApplicationCode()
        {
            string code = $"IRA{DateTime.Now.ToString("yyyyMMdd")}";

            Inventory_Request_Application lastDailyApplication = repository.GetLastDailyApplication();

            int num = 1;

            if (lastDailyApplication != null)
            {
                string formCode = lastDailyApplication.ir_application_code;

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