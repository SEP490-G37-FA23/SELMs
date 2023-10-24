using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface IEquipmentService
    {
        Task SaveEquipment(Equipment equipment);
        Task ImportEquipments(List<Equipment> equipments);
        Task UpdateEquipment(int id, Equipment equipment);
    }
}
