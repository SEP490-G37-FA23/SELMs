using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Api.DTOs;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class CategoryService : ICategoryService
	{
		private ICategoryRepository repository = new CategoryRepository();
		private IEquipmentRepository equipmentRepos = new EquipmentRepository();

        public async Task<List<Equipment>> GetListSystemEquipCodeFromListStanrdCode(List<StandardEquipmentDTO> equipments)
        {
			List<Equipment> listSystemEquip = new List<Equipment>();
			foreach(var equip in equipments)
			{
                List<Equipment> temp = await repository.GetListSystemCodeFromStandCode(equip.standard_equipment_code);
                foreach(var item in temp)
				{
                    listSystemEquip.Add(item);
                }
            }
			return listSystemEquip;
			
        }

        public async Task SaveCategory(Category category, List<StandardEquipmentDTO> equipments)
		{
			Category obj = repository.SaveCategory(category);
			foreach (StandardEquipmentDTO equipment in equipments)
			{
				equipment.category_code = obj.category_code;
			}
			equipmentRepos.SaveEquipmentsToCategory(equipments);
		}

		public async Task UpdateCategory(int id, Category category)
		{
			if (repository.GetCategory(id) != null)
			{
				category.category_id = id;
				repository.UpdateCategory(category);
			}
		}
	}
}