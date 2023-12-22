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
    
    public partial class Equipment_Handover_Form
    {
        public int form_id { get; set; }
        public string form_code { get; set; }
        public System.DateTime create_date { get; set; }
        public string creater { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> handover_date { get; set; }
        public string handover { get; set; }
        public Nullable<System.DateTime> receipt_date { get; set; }
        public string receipter { get; set; }
        public string handover_note { get; set; }
        public bool is_finish { get; set; }
        public Nullable<int> location_id { get; set; }
        public Nullable<int> project_id { get; set; }
    
        public virtual Location Location { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
