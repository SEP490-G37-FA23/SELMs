using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class CategoryRequest
    {
        public CategoryDTO category { get; set; }
        public List<EquipmentDTO> equipments { get; set; }
    }
}