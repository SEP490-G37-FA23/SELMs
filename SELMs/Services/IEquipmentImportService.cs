using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services
{
    public interface IEquipmentImportService
    {
        Task<ApplicationModel> GetApplication(int id); 
        Task SaveApplication(Equipment_Import_Application application, List<Equipment_Import_Application_Detail> applicationDetails);
        Task UpdateApplication(int id, Equipment_Import_Application application, List<Equipment_Import_Application_Detail> applicationDetails);
    }
}