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
    
    public partial class Equipment_Import_Application
    {
        public int application_id { get; set; }
        public string ei_application_code { get; set; }
        public Nullable<System.DateTime> application_date { get; set; }
        public string created_by { get; set; }
        public string supplier { get; set; }
        public Nullable<int> total_equipments { get; set; }
        public Nullable<decimal> total_price { get; set; }
        public string desciption { get; set; }
        public string note { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> approved_date { get; set; }
        public string approver { get; set; }
        public string approver_notes { get; set; }
    }
}
