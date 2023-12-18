using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class ProjectDTO
    {
        public int project_id { get; set; }
        public string project_code { get; set; }
        public string project_name { get; set; }
        public string acronym { get; set; }
        public string description { get; set; }
        public string manager { get; set; }
        //public Nullable<System.DateTime> start_date { get; set; }
        public string start_date { get; set; }
        //public Nullable<System.DateTime> end_date { get; set; }
        public string end_date { get; set; }
        //public Nullable<System.DateTime> create_date { get; set; }
        public DateTime? create_date { get; set; }
        public string creater { get; set; }
        public bool status { get; set; }
        public Nullable<int> location_id { get; set; }
    }
}