using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IEquipmentHandoverFormRepository
    {
        dynamic GetApplicationList();
        dynamic GetApplicationList(Argument arg);
        dynamic GetApplication(int id);
        dynamic GetLastDailyApplication();
        dynamic SaveApplication(Equipment_Handover_Form application);
        dynamic UpdateApplication(Equipment_Handover_Form application);
        void DeleteApplication(int id);
        dynamic GetApplicationDetailList(string applicationCode);
        dynamic GetApplicationDetailList(Argument arg);
        dynamic GetApplicationDetail(int id);
        dynamic GetApplicationDetailById(int id);
        dynamic SaveApplicationDetail(Equipment_Handover_Form_Detail applicationDetail);
        dynamic SaveApplicationDetails(List<Equipment_Handover_Form_Detail> applicationDetails);
        void UpdateApplicationDetail(Equipment_Handover_Form_Detail applicationDetail);
        void DeleteApplicationDetail(int id);
        dynamic GetApplicationAttachment(int applicationId);
        dynamic AddAttachment(int applicationId, int attachmentId);
        void DeleteAttachment(int applicationId, int attachmentId);
    }
}
