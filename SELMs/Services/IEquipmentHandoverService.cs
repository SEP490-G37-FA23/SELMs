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
        Task SaveApplication(Equipment_Handover_Form application, List<Equipment_Handover_Form_Detail> applicationDetails, HttpPostedFileBase attachment);
        Task UpdateApplication(int id, Equipment_Handover_Form application, List<Equipment_Handover_Form_Detail> applicationDetails, HttpPostedFileBase attachments);
    }
}
