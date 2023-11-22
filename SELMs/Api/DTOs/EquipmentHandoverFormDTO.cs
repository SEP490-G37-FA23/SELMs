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
        //public System.DateTime create_date { get; set; }
        public string create_date { get; set; }
        public string creater { get; set; }
        public string ea_application_code { get; set; }
        public string description { get; set; }
        //public Nullable<System.DateTime> handover_date { get; set; }
        public string handover_date { get; set; }
        public string handover { get; set; }
        public bool is_confirmed_handover { get; set; }
        //public Nullable<System.DateTime> receipt_date { get; set; }
        public string receipt_date { get; set; }
        public string receipter { get; set; }
        public bool is_confirmed_recipienter { get; set; }
        public string handover_note { get; set; }
        public bool is_finish { get; set; }
    }
}