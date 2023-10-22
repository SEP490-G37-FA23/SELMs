using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteCategory(int id)
        {
            dynamic category = db.Categories.Where(c => c.category_id == id).FirstOrDefault();
            if(category != null) db.Categories.Remove(category);
            db.SaveChangesAsync();
        }

        public dynamic GetCategory(int id)
        {
            dynamic category = db.Categories.Where(c => c.category_id == id).FirstOrDefault();
            return category;
        }

        public dynamic GetCategoryList()
        {
            dynamic categories = db.Categories.ToListAsync();
            return categories;
        }

        public void SaveCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChangesAsync();
        }

        public dynamic SearchCategories(Argument arg)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            Category orgCategory = db.Categories.Where(c => c.category_id == category.category_id).FirstOrDefault();
            db.Entry(orgCategory).CurrentValues.SetValues(category);
            db.SaveChangesAsync();
        }
    }
}