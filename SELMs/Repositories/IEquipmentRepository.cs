using SELMs.Api.DTOs;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IEquipmentRepository
    {
        dynamic GetEquipmentList();
        dynamic GetEquipmentList(Argument arg);
        dynamic GetEquipment(int id);
        dynamic GetDetailEquipment(string code);
        void SaveEquipment(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips);
        void SaveEquipments(List<Equipment> equipments);
        void UpdateEquipment(Equipment equipment);
        void DeleteEquipment(int id);
        Equipment GetLastEquipment();

        List<EquipComponentDTO> GetListComponentEquips(string code);
    }
}
