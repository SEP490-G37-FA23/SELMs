using SELMs.Api.DTOs;
using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services
{
    public interface IEquipmentService
    {
        Task<dynamic> GetEquipment(int id);
        Task<dynamic> SaveEquipment(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips);
        Task ImportEquipments(List<Equipment> equipments);
        Task UpdateEquipment(int id, Equipment equipment);
        Task<dynamic> AddImages(int id, List<HttpPostedFile> images);

    }
}
