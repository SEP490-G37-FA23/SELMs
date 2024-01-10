using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentHandoverFormDTO
    {
        public int form_id { get; set; }
        public string form_code { get; set; }
        public string create_date { get; set; }
        public string creater { get; set; }
        public string description { get; set; }
        public string handover_date { get; set; }
        public string handover { get; set; }
        public string finish_date { get; set; }
        public string receipter { get; set; }
        public string handover_note { get; set; }
        public bool is_finish { get; set; }
        public int location_id { get; set; }
        public int project_id { get; set; }
    }
}