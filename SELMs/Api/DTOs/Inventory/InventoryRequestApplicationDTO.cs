using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class InventoryRequestApplicationDTO
    {
        public int? application_id { get; set; }
        public string ir_application_code { get; set; }
        public string requester { get; set; }
        public DateTime request_date { get; set; } = DateTime.Now;
        public string performer { get; set; }
        public int total_equipment { get; set; }
        public int location_id { get; set; }
        public bool status { get; set; } = false;
        public string type_inventory { get; set; }
    }
}