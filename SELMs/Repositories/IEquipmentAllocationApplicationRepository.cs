using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IEquipmentAllocationApplicationRepository
    {
        dynamic GetApplicationList();
        dynamic GetApplicationList(Argument arg);
        dynamic GetApplication(int id);
        dynamic GetLastDailyApplication();
        dynamic SaveApplication(Equipment_Allocation_Application application);
        void UpdateApplication(Equipment_Allocation_Application application);
        void DeleteApplication(int id);
        dynamic GetApplicationDetailList(string applicationCode);
        dynamic GetApplicationDetailList(Argument arg);
        dynamic GetApplicationDetail(int id);
        dynamic SaveApplicationDetail(Equipment_Allocation_Application_Detail application);
        dynamic SaveApplicationDetails(List<Equipment_Allocation_Application_Detail> applications);
        void UpdateApplicationDetail(Equipment_Allocation_Application_Detail application);
        void DeleteApplicationDetail(int id);
        dynamic GetEAAList(Argument arg);
        dynamic GetAvailableEquipmentList(Argument arg);
        dynamic GetNeedImportEquipmentList(Argument arg);
    }
}
