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
        Task<dynamic> GetEquipment(int id);
        dynamic GetDetailEquipment(string code);
        dynamic SaveEquipment(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips);
        dynamic SaveEquipments(List<Equipment> equipments);
        Task UpdateEquipment(Equipment equipment);
        void DeleteEquipment(int id);
        Equipment GetLastEquipment();

        List<EquipComponentDTO> GetListComponentEquips(string code);
        dynamic GetStandardEquipmentList(Argument args);
        void SaveEquipmentsToCategory(List<StandardEquipmentDTO> equipments);
        dynamic GetEquipmentImages(int id);
    }
}
