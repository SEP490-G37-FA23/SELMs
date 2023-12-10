using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs.Inventory
{
    public class PerformInventoryDTO
    {
        public string username { get; set; }
        public List<PerformInventoryDetailDTO> listPerform { get; set; }
    }
    public class PerformInventoryDetailDTO
    {
       public int  application_detail_id { get; set; }
        public string  system_equipment_code { get; set; }
        public string actual_usage_status { get; set; }
        public bool is_perform { get; set; }
    }
}