using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using SELMs.Api.DTOs;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository repository = new EquipmentRepository();
        private IImageRepository imageRepository = new ImageRepository();
        private readonly IEquipmentLocationHistoryRepository equipmentLocationHistoryRepository = new EquipmentLocationHistoryRepository();

        public async Task<dynamic> GetEquipment(int id)
        {
            Equipment equipment = await repository.GetEquipment(id);
            List<Image> images = repository.GetEquipmentImages(id);
            List<EquipComponentDTO> components = repository.GetListComponentEquips(equipment.system_equipment_code);
            EquipmentModel equipmentModel = new EquipmentModel()
            {
                equipment = equipment,
                equipment_components = components,
                equipment_images = images
            };
            return equipmentModel;
        }

        public async Task ImportEquipments(List<Equipment> equipments, string username)
        {
            List<EquipComponentDTO> ListComponentEquips = new List<EquipComponentDTO>();
            foreach (Equipment equip in equipments)
            {
                equip.create_date = DateTime.Now;
                equip.responsibler = username;
                await SaveEquipment(equip, 2, ListComponentEquips);
            }
        }


        public async Task<dynamic> SaveEquipment(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips)
        {
            Equipment eq = equipment;
            string code = GenerateEquipmentCode();
            eq.system_equipment_code = code;
            eq = repository.SaveEquipment(eq, location_id, ListComponentEquips);
            return eq;
        }

        public async Task<dynamic> UpdateEquipment(int id, Equipment equipment)
        {
            if (repository.GetEquipment(id) != null)
            {
                equipment.equipment_id = id;
                equipment = repository.UpdateEquipment(equipment);
            }
            return equipment;
        }

        public async Task<dynamic> AddImages(int id, List<HttpPostedFile> files)
        {
            Equipment equipment = await repository.GetEquipment(id);
            int order = 1;
            List<Image> imgs = new List<Image>();
            foreach (HttpPostedFile item in files)
            {
                if (item.ContentType.StartsWith("image"))
                {
                    Image img = new Image()
                    {
                        equipment_id = equipment.equipment_id,
                        date = DateTime.Now,
                        image_name = $"{equipment.system_equipment_code}_{DateTime.Now.ToString("yyyyMMdd")}_{order}",
                        content = new BinaryReader(item.InputStream).ReadBytes(item.ContentLength)
                    };
                    imgs.Add(img);
                }
                order++;
            }
            imgs = imgs.Count > 0 ? imageRepository.SaveImages(imgs) : new List<Image>();
            return imgs;
        }


        string GenerateEquipmentCode()
        {
            string eqmtCode = "E";
            Equipment equipment = repository.GetLastEquipment();
            int num = equipment == null ? 1 : Convert.ToInt32(equipment.system_equipment_code.Replace("E","")) + 1;
            eqmtCode += num < 10000 ? num.ToString("D4") : num.ToString();
            return eqmtCode;
        }
    }
}