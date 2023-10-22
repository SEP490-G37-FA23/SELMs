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
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository repository = new CategoryRepository();
        public async Task UpdateCategory(int id, Category category)
        {
            if (await repository.GetCategory(id) != null)
            {
                category.category_id = id;
                repository.UpdateCategory(category);
            }
        }
    }
}