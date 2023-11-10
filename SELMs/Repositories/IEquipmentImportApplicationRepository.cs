using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IEquipmentImportApplicationRepository
    {
        dynamic GetApplicationList();
        dynamic GetApplicationList(Argument arg);
        dynamic GetApplication(int id);
        dynamic GetLastDailyApplication();
        dynamic SaveApplication(Equipment_Import_Application application);
        void UpdateApplication(Equipment_Import_Application application);
        void DeleteApplication(int id);

        dynamic GetApplicationDetailList();
        dynamic GetApplicationDetailList(string applicationCode);
        dynamic GetApplicationDetailList(Argument arg);
        dynamic GetApplicationDetail(int id);
        dynamic SaveApplicationDetail(Equipment_Import_Application_Detail applicationDetail);
        dynamic SaveApplicationDetails(List<Equipment_Import_Application_Detail> applicationDetails);
        void UpdateApplicationDetail(Equipment_Import_Application_Detail applicationDetail);
        void DeleteApplicationDetail(int id);
    }
}
