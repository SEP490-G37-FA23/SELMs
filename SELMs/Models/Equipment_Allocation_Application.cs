//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SELMs.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipment_Allocation_Application
    {
        public int application_id { get; set; }
        public string ea_application_code { get; set; }
        public System.DateTime application_date { get; set; }
        public string application_processer { get; set; }
        public string creater { get; set; }
        public Nullable<int> project_id { get; set; }
        public int total_equipments { get; set; }
        public string notes { get; set; }
        public string desciption { get; set; }
        public string approver_note { get; set; }
        public Nullable<System.DateTime> approved_date { get; set; }
        public string approver { get; set; }
        public string status { get; set; }
    }
}
