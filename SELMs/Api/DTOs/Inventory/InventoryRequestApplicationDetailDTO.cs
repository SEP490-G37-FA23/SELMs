using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class InventoryRequestApplicationDetailDTO
    {
        public int application_detail_id { get; set; }
        public string ir_application_code { get; set; }
        public string system_equipment_code { get; set; }
        public string inventory_results { get; set; }
        public string actual_usage_status { get; set; }
        //public Nullable<System.DateTime> inventory_date { get; set; }
        public string inventory_date { get; set; }
        public bool is_perform { get; set; }
    }
}