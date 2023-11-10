using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class CategoryService : ICategoryService
	{
		private ICategoryRepository repository = new CategoryRepository();
		private IEquipmentRepository equipmentRepos = new EquipmentRepository();

		public async Task SaveCategory(Category category, List<Equipment> equipments)
		{
			Category obj = repository.SaveCategory(category);
			foreach (Equipment equipment in equipments)
			{
				equipment.category_code = obj.category_code;
			}
			equipmentRepos.SaveEquipments(equipments);
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