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
using SELMs.Api.DTOs.Inventory;
using static System.Net.Mime.MediaTypeNames;
using AutoMapper;
using SELMs.App_Start;

namespace SELMs.Services.Implements
{
    public class InventoryRequestService : IInventoryRequestService
    {
        private IInventoryRequestApplicationRepository repository = new InventoryRequestApplicationRepository();
        private IAttachmentRepository attachmentRepository = new AttachmentRepository();
        private IEquipmentRepository equipmentRepository = new EquipmentRepository();
        private IMapper mapper = MapperConfig.Initialize();

        public async Task<ApplicationModel> GetApplication(int id)
        {
            Inventory_Request_Application application = await repository.GetApplication(id);
            List<Inventory_Request_Application> applicationDetails = await repository.GetApplicationDetailList(application.ir_application_code);
            //Attachment attachment = repository.GetApplicationAttachment(application.application_id);
            ApplicationModel result = new ApplicationModel()
            {
                application = application,
                applicationDetails = applicationDetails,
                //attachment = attachment
            };
            return result;
        }

        public async Task<dynamic> SaveApplication(Inventory_Request_Application application, List<string> applicationDetails)
        {
            string applicationCode = GenerateApplicationCode();
            application.ir_application_code = applicationCode;
            application.request_date = DateTime.Now.ToString();
            application.status = false;
            application = repository.SaveApplication(application);
            Attachment attachment = new Attachment();
            foreach (string item in applicationDetails)
            {
                Inventory_Request_Application_Detail detail = new Inventory_Request_Application_Detail();
                detail.ir_application_code = applicationCode;
                detail.system_equipment_code = item;
                detail.is_perform = false;
                repository.SaveApplicationDetail(detail);
            }            
            
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
            Inventory_Request_Application application = await repository.GetApplication(id);
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
            Inventory_Request_Application application = await repository.GetApplication(id);
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

        public async  Task<dynamic> PerformInventoryRequest(PerformInventoryDTO perform)
        {
            foreach (PerformInventoryDetailDTO item in perform.listPerform)
            {
                Inventory_Request_Application_Detail detail = await repository.GetApplicationDetail(item.application_detail_id);
                detail.inventory_date = DateTime.Now;
                detail.actual_usage_status = item.actual_usage_status;
                detail.is_perform = item.is_perform;
                await repository.UpdateApplicationDetail(detail);
            }
            return "Thành công";
        }

        public async Task<dynamic> UpdateEquipmentResult(UpdateEquipmentResultDTO updateEquip)
        {
            foreach (UpdateDetailDTO item in updateEquip.listUpdate)
            {
                Inventory_Request_Application_Detail detail = await repository.GetApplicationDetail(item.application_detail_id);
                detail.inventory_results = item.inventory_results;
                detail.actual_usage_status = item.actual_usage_status;
                var applicationDetail = repository.UpdateApplicationDetail(detail);

                Equipment equip = await equipmentRepository.GetEquipment(item.equipment_id);
                equip.usage_status = item.actual_usage_status;
                var equipment = equipmentRepository.UpdateEquipment(equip);
            }
            return "Thành công";
        }
    }
}