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
    public class EquipmentImportService : IEquipmentImportService
    {
        private IEquipmentImportApplicationRepository repository = new EquipmentImportApplicationRepository();
        public async Task<ApplicationModel> GetApplication(int id)
        {
            Equipment_Import_Application application = repository.GetApplication(id);
            List<Equipment_Import_Application> applicationDetails = repository.GetApplicationDetailList(application.ei_application_code);
            ApplicationModel result = new ApplicationModel()
            {
                application = application,
                applicationDetails = applicationDetails
            };
            return result;
        }

        public async Task SaveApplication(Equipment_Import_Application application, List<Equipment_Import_Application_Detail> applicationDetails)
        {
            string applicationCode = GenerateApplicationCode();
            application.ei_application_code = applicationCode;
            application.application_date = DateTime.Now;
            application.status = ApplicationStatus.PENDING;
            foreach(Equipment_Import_Application_Detail item in applicationDetails)
            {
                item.ei_application_code = applicationCode;
            }
            repository.SaveApplication(application);
            repository.SaveApplicationDetails(applicationDetails);
        }



        public Task UpdateApplication(Equipment_Import_Application application, List<Equipment_Import_Application_Detail> applicationDetails)
        {
            throw new NotImplementedException();
        }

        string GenerateApplicationCode()
        {
            string code = $"EIA{DateTime.Now.ToString("yyyyMMdd")}";
            Equipment_Import_Application lastDailyApplication = repository.GetLastDailyApplication();
            if (lastDailyApplication != null)
            {
                string num = lastDailyApplication.ei_application_code.Replace(code, "");
                if (num.Count() > 0)
                {
                    num = (Convert.ToInt32(num) + 1).ToString();
                    code += num;
                }
                else
                {
                    code += "1";
                }
            }
            return code;
        }
    }
}