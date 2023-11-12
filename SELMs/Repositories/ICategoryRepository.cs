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

        dynamic getCategoryEquipments(string categoryCode);
    }
}
