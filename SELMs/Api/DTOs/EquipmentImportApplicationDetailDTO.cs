using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentImportApplicationDetailDTO
    {
        public int application_detail_id { get; set; }
        public string ei_application_code { get; set; }
        public string system_equipment_code { get; set; }
        public string standard_equipment_code { get; set; }
        public string equipment_name { get; set; }
        public string equipment_specification { get; set; }
        public string unit { get; set; }
        public string category_code { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> total { get; set; }
        public string note { get; set; }
    }
}