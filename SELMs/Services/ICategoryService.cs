using SELMs.Api.DTOs;
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
        Task DeactivateCategory(int id);
        Task SaveCategory(Category category, List<StandardEquipmentDTO> equipments);
        Task UpdateCategory(int id, Category category);
    }
}
