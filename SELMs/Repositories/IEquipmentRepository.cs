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
        dynamic SearchEquipments(Argument arg);
        dynamic GetEquipment(int id);
        void SaveEquipment(Equipment equipment);
        void SaveEquipments(List<Equipment> equipments);
        void UpdateEquipment(Equipment equipment);
        void DeleteEquipment(int id);
        Equipment GetLastEquipment();
    }
}
