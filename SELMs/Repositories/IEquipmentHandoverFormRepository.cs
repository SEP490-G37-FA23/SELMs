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
        dynamic SaveApplication(Equipment_Handover_Form application);
        void UpdateApplication(Equipment_Handover_Form application);
        void DeleteApplication(int id);
        dynamic GetApplicationDetailList();
        dynamic GetApplicationDetailList(Argument arg);
        dynamic GetApplicationDetail(int id);
        dynamic SaveApplicationDetail(Equipment_Handover_Form_Detail application);
        dynamic SaveApplicationDetails(List<Equipment_Handover_Form_Detail> applications);
        void UpdateApplicationDetail(Equipment_Handover_Form_Detail application);
        void DeleteApplicationDetail(int id);
    }
}
