using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentAllocationApplicationDetailDTO
    {
        public int application_detail_id { get; set; }
        public string ea_application_code { get; set; }
        public string system_equipment_code { get; set; }
        public string standard_equipment_code { get; set; }
        public string equipment_name { get; set; }
        public string equipment_specification { get; set; }
        public string unit { get; set; }
        public Nullable<int> quantity { get; set; }
        public string usage_status { get; set; }
        public string note { get; set; }
        public string status { get; set; }
    }
}