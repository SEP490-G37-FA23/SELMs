using SELMs.Models.BusinessModel;
using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SELMs.Api.DTOs.Inventory;

namespace SELMs.Services
{
    public interface IInventoryRequestService
    {
        Task<ApplicationModel> GetApplication(int id);
        Task<dynamic> SaveApplication(Inventory_Request_Application application, List<string> applicationDetails);
        Task<dynamic> UpdateApplication(int id, Inventory_Request_Application application, List<Inventory_Request_Application_Detail> applicationDetails);
        //Task<dynamic> AddAttachment(int id, HttpPostedFileBase file);
        //Task DeleteAttachment(int applicationId, int attachmentId);
        Task<dynamic> ConfirmApplication(int id, User member);
        Task CancelApplication(int id);
        Task<dynamic> PerformInventoryRequest(PerformInventoryDTO perform);
    }
}
