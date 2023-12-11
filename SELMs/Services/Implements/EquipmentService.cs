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

        public async Task<dynamic> GetEquipment(int id)
        {
            Equipment equipment = repository.GetEquipment(id);
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

        public async Task ImportEquipments(List<Equipment> equipments)
        {
            Equipment obj = repository.GetLastEquipment();
            int num = obj == null ? 1 : obj.equipment_id;
            foreach (Equipment equip in equipments)
            {
                string code = "E";
                code += num < 10000 ? num.ToString("D4") : num.ToString();
                equip.system_equipment_code = code;
            }
            repository.SaveEquipments(equipments);
        }


        public async Task<dynamic> SaveEquipment(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips)
        {
            Equipment eq = equipment;
            string code = GenerateEquipmentCode();
            eq.system_equipment_code = code;
            eq = repository.SaveEquipment(eq, location_id, ListComponentEquips);
            return eq;
        }

        public async Task UpdateEquipment(int id, Equipment equipment)
        {
            if (repository.GetEquipment(id) != null)
            {
                equipment.equipment_id = id;
                repository.UpdateEquipment(equipment);
            }
        }

        public async Task<dynamic> AddImages(int id, List<HttpPostedFile> files)
        {
            Equipment equipment = repository.GetEquipment(id);
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

            }
            imgs = imgs.Count > 0 ? imageRepository.SaveImages(imgs) : imgs;
            return imgs;
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