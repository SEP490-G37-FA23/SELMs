using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs.Inventory
{
    public class UpdateEquipmentResultDTO
    {
        public string username { get; set;}
        public List<UpdateDetailDTO> listUpdate { get; set;}
    }

    public class UpdateDetailDTO
    {
        public string actual_usage_status { get; set; }
        public int equipment_id { get; set; }
        public string inventory_results { get; set; }
        public int application_detail_id { get; set; }
    }
}