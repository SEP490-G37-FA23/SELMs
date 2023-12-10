using SELMs.Models.BusinessModel;
using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IInventoryRequestApplicationRepository
    {
        dynamic GetApplicationList();
        dynamic GetApplicationList(Argument arg);
        dynamic GetApplication(int id);
        dynamic GetLastDailyApplication();
        dynamic SaveApplication(Inventory_Request_Application application);
        dynamic UpdateApplication(Inventory_Request_Application application);
        void DeleteApplication(int id);
        dynamic GetApplicationDetailList(string applicationCode);
        dynamic GetApplicationDetailList(Argument arg);
        Task<Inventory_Request_Application_Detail> GetApplicationDetail(int id);
        dynamic SaveApplicationDetail(Inventory_Request_Application_Detail applicationDetail);
        dynamic SaveApplicationDetails(List<Inventory_Request_Application_Detail> applicationDetails);
        Task UpdateApplicationDetail(Inventory_Request_Application_Detail applicationDetail);
        void DeleteApplicationDetail(int id);
        dynamic GetDetailIRAListInLocation(int location_id, Argument arg);
        dynamic GetResultIRAList(Argument arg);
    }
}
