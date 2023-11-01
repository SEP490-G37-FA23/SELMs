using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SELMs.Api.DTOs
{
    public class EquipmentDTO
    {
        public int? equipment_id { get; set; }
        public string system_equipment_code { get; set; }
        public string standard_equipment_code { get; set; }
        public string equipment_name { get; set; }
        public string serial_no { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public string unit { get; set; }
        public string specification { get; set; }
        public string responsibler { get; set; }
        public string usage_status { get; set; }
        public string type_equipment { get; set; }
        public int location_id { get; set; }

        public string note { get; set; }
        public decimal price { get; set; }

        public string category_code { get; set; }
        public List<int> img { get; set; }
      public bool is_integration { get;set; }

    }


    public class EquipmentNew
    {
        public EquipmentDTO equip { get; set; }
        public List<EquipComponentDTO> ListComponentEquips { get; set; }
        public int location_id { get; set; }
        public List<string> img { get; set; }


    }

    public class DetailEquipDTO
    {
        public dynamic equip { get; set; }
        public List<EquipComponentDTO> ListComponentEquips { get; set; }


    }

}
