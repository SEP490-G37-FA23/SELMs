using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface ICategoryService
    {
        Task SaveCategory(Category category, List<Equipment> equipments);
        Task UpdateCategory(int id, Category category);
    }
}
