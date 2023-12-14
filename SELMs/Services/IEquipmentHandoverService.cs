using SELMs.Models.BusinessModel;
using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services
{
    public interface IEquipmentHandoverService
    {
        Task<ApplicationModel> GetApplication(int id);
        Task<dynamic> SaveApplication(Equipment_Handover_Form application, List<Equipment_Handover_Form_Detail> applicationDetails);
        Task<dynamic> UpdateApplication(int id, Equipment_Handover_Form application, List<Equipment_Handover_Form_Detail> applicationDetails);
        Task<dynamic> AddAttachment(int id, HttpPostedFile file);
        Task DeleteAttachment(int applicationId, int attachmentId);
        Task<dynamic> ConfirmApplication(int id, User member);
        Task CancelApplication(int id);
    }
}
