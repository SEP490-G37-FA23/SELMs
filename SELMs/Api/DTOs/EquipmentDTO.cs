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
        public string equipment_code { get; set; }
        public string equipment_name { get; set; }
        public string serial_no { get; set; }
        public Nullable<System.DateTime> import_date { get; set; }
        public string unit { get; set; }
        public string specification { get; set; }
        public string img { get; set; }
        public string location { get; set; }
        public string responsible_person { get; set; }
        public string status { get; set; }
        public string note { get; set; }
    }
}
