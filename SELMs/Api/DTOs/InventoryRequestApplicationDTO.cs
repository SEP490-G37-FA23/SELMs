using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class InventoryRequestApplicationDTO
    {
        public int application_id { get; set; }
        public string ir_application_code { get; set; }
        public string requester { get; set; }
        public string request_date { get; set; }
        public string performer { get; set; }
        public int total_equipment { get; set; }
        public bool status { get; set; }
        public string type_inventory { get; set; }
        public Nullable<int> location_id { get; set; }
    }
}