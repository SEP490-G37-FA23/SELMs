using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface IEquipmentAllocationService
    {
        Task<ApplicationModel> GetApplication(int id);
        Task SaveApplication(Equipment_Allocation_Application application, List<Equipment_Allocation_Application_Detail> applicationDetails);
        Task UpdateApplication(int id, Equipment_Allocation_Application application, List<Equipment_Allocation_Application_Detail> applicationDetails);
    }
}
