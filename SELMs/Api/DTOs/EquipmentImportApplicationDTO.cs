using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentImportApplicationDTO
    {
        public int application_id { get; set; }
        public string ei_application_code { get; set; }
        //public Nullable<System.DateTime> application_date { get; set; }
        public string application_date { get; set; }
        public string created_by { get; set; }
        public string supplier { get; set; }
        public Nullable<int> total_equipments { get; set; }
        public Nullable<decimal> total_price { get; set; }
        public string desciption { get; set; }
        public string note { get; set; }
        public string status { get; set; }
        //public Nullable<System.DateTime> approved_date { get; set; }
        public string approved_date { get; set; }
        public string approver { get; set; }
        public string approver_notes { get; set; }
    }
}