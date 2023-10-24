using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Api.DTOs
{
    public class EquipmentDTO
    {
        public int? equipment_id { get; set; }
        public string system_equipment_code { get; set; }
        public string standard_equipment_code { get; set; }
        public string equipment_name { get; set; }
        public Nullable<int> equipment_img { get; set; }
        public string serial_no { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public string unit { get; set; }
        public string specification { get; set; }
        public Nullable<int> img { get; set; }
        public string responsibler { get; set; }
        public string usage_status { get; set; }
        public string type_equipment { get; set; }
        public string note { get; set; }
        public string category_code { get; set; }
    }
}
