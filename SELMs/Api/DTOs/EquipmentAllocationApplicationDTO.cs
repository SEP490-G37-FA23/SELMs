﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentAllocationApplicationDTO
    {
        public int application_id { get; set; }
        public string ea_application_code { get; set; }
        //public System.DateTime application_date { get; set; }
        public string application_date { get; set; }
        public string creater { get; set; }
        public int total_equipments { get; set; }
        public string notes { get; set; }
        public string desciption { get; set; }
        public string approver_note { get; set; }
        public string approved_date { get; set; }
        public string approver { get; set; }
        public string status { get; set; }
        public int location_id { get; set; }
        public int project_id { get; set; }

    }
}