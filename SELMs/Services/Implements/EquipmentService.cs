using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services.Implements
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository repository = new EquipmentRepository();
        public async Task ImportEquipments(List<Equipment> equipments)
        {
            Equipment obj = repository.GetLastEquipment();
            int num = obj == null ? 1 : obj.equipment_id;
            foreach (Equipment equip in equipments)
            {
                string code = "E";
                code += num < 10000 ? num.ToString("D4") : num.ToString();
                equip.equipment_code = code;
            }
            repository.SaveEquipments(equipments);
        }

        public async Task SaveEquipment(Equipment equipment)
        {
            Equipment eq = equipment;
            string code = GenerateEquipmentCode();
            eq.equipment_code = code;
            repository.SaveEquipment(eq);
        }

        public async Task UpdateEquipment(int id, Equipment equipment)
        {
            if (await repository.GetEquipment(id) != null)
            {
                equipment.equipment_id = id;
                repository.UpdateEquipment(equipment);
            }
        }

        string GenerateEquipmentCode()
        {
            string eqmtCode = "E";
            dynamic equipment = repository.GetLastEquipment();
            int num = equipment == null ? 1 : equipment.equipment_id + 1;
            eqmtCode += num < 10000 ? num.ToString("D4") : num.ToString();
            return eqmtCode;
        }
    }
}