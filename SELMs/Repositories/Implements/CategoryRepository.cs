using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteCategory(int id)
        {
            dynamic category = db.Categories.Where(c => c.category_id == id).FirstOrDefault();
            if (category != null) db.Categories.Remove(category);
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

        public dynamic SaveCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }

        public dynamic SearchCategories(Argument arg)
        {
            dynamic categories = null;
            categories = db.Database.Connection.QueryAsync<dynamic>("Proc_GetCategoryList", new
            {
                username = arg.username,
                isadmin = arg.isadmin,
                role = arg.role,
                text = arg.text
            }
                , commandType: CommandType.StoredProcedure);
            return categories;
        }

        public void UpdateCategory(Category category)
        {
            Category orgCategory = db.Categories.Where(c => c.category_id == category.category_id).FirstOrDefault();
            db.Entry(orgCategory).CurrentValues.SetValues(category);
            db.SaveChangesAsync();
        }


        public List<Equipment> getCategoryEquipments(string categoryCode)
        {
            List<Equipment> equipments =  db.Equipments.Where(e => e.category_code == categoryCode).ToList();
            return equipments;
        }

        public async Task<dynamic> GetParentCategoriesList(Argument arg)
        {
            dynamic parentCategories = null;
            parentCategories = await db.Database.Connection.QueryAsync<dynamic>("Proc_GetParentCategoriesList", new
            {
                level = arg.level
            }
                , commandType: CommandType.StoredProcedure);
            return parentCategories;
        }
        public async Task<List<Equipment>> GetListSystemCodeFromStandCode(string standard_equipment_code)
        {
            return await db.Equipments.Where(c => c.standard_equipment_code == standard_equipment_code).ToListAsync();
        }
    }
}