using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface ICategoryRepository
    {
        dynamic GetCategoryList();
        dynamic SearchCategories(Argument arg);
        dynamic GetCategory(int id);
        dynamic SaveCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);

        List<Equipment> getCategoryEquipments(string categoryCode);
        Task<dynamic> GetParentCategoriesList(Argument arg);
        Task<List<Equipment>> GetListSystemCodeFromStandCode(string standard_equipment_code);
    }
}
