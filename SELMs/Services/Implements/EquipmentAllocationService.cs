using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using log4net;

namespace SELMs.Services.Implements
{
    public class EquipmentAllocationService : IEquipmentAllocationService
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
            string codePrefix = $"EAA{DateTime.Now.ToString("yyyyMMdd")}";
            Equipment_Allocation_Application lastDailyApplication = repository.GetLastDailyApplication();

            int num;

            if (lastDailyApplication != null && lastDailyApplication.ea_application_code.StartsWith(codePrefix))
            {
                // Extract the numeric part from the existing code and increment it.
                string numericPart = lastDailyApplication.ea_application_code.Substring(codePrefix.Length);
                num = int.Parse(numericPart) + 1;
            }
            else
            {
                // If there is no lastDailyApplication or the code does not start with the expected prefix,
                // start with 1.
                num = 1;
            }

            // Build the new code.
            string newCode = codePrefix + (num < 10000 ? num.ToString("D4") : num.ToString());

            return newCode;
        }


        public async Task<dynamic> ApproveApplication(string code)
        {
            try
            {
                Equipment_Allocation_Application application = repository.GetApplicationByCode(code);
                if (application != null)
                {
                    application.status = ApplicationStatus.APPROVED;
                    var result = repository.UpdateApplication(application);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }


    }
}