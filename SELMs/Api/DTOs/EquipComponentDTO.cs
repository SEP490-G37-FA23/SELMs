using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Api.DTOs
{
    public class EquipComponentDTO
    {
        public string system_equipment_code { get; set; }
        public string standard_equipment_code { get; set; }
        public string equipment_name { get; set; }
        public string usage_status { get; set; }
    }
}