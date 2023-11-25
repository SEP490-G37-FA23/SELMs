using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class CategoryRequest
    {
        public CategoryDTO category { get; set; }
        public List<StandardEquipmentDTO> equipments { get; set; }
    }

    public class StandardEquipmentDTO
    {
        public string standard_equipment_code { get; set; }
        public string equipment_name { get; set; }=null;
        public string category_code { get; set; } = null;


    }
}