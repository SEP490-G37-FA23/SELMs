using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
    public class EquipmentAllocationService: IEquipmentAllocationService
    {
        private IEquipmentAllocationApplicationRepository repository = new EquipmentAllocationApplicationRepository();
        public async Task<ApplicationModel> GetApplication(int id)
        {
            Equipment_Allocation_Application application = repository.GetApplication(id);
            List<Equipment_Allocation_Application> applicationDetails = repository.GetApplicationDetailList(application.ea_application_code);
            ApplicationModel result = new ApplicationModel()
            {
                application = application,
                applicationDetails = applicationDetails
            };
            return result;
        }

        public async Task SaveApplication(Equipment_Allocation_Application application, List<Equipment_Allocation_Application_Detail> applicationDetails)
        {
            string applicationCode = GenerateApplicationCode();
            application.ea_application_code = applicationCode;
            application.application_date = DateTime.Now;
            application.status = ApplicationStatus.PENDING;
            foreach (Equipment_Allocation_Application_Detail item in applicationDetails)
            {
                item.ea_application_code = applicationCode;
            }
            repository.SaveApplication(application);
            repository.SaveApplicationDetails(applicationDetails);
        }

        public async Task UpdateApplication(int id, Equipment_Allocation_Application application, List<Equipment_Allocation_Application_Detail> applicationDetails)
        {
            if (repository.GetApplication(id) != null)
            {
                application.application_id = id;
                repository.UpdateApplication(application);
            }
            foreach (Equipment_Allocation_Application_Detail item in applicationDetails)
            {
                if (item.application_detail_id != null && repository.GetApplicationDetail(item.application_detail_id) != null)
                {
                    repository.UpdateApplicationDetail(item);
                }
                else
                {
                    item.ea_application_code = application.ea_application_code;
                    repository.SaveApplicationDetail(item);
                }
            }
        }

        string GenerateApplicationCode()
        {
            string code = $"EAA{DateTime.Now.ToString("yyyyMMdd")}";
            Equipment_Allocation_Application lastDailyApplication = repository.GetLastDailyApplication();

            int num = lastDailyApplication == null ? 1 : Convert.ToInt32((lastDailyApplication.ea_application_code).Replace(code, "")) + 1;
            code +=  num < 10000 ? num.ToString("D4") : num.ToString();
            return code;
        }


    }
}