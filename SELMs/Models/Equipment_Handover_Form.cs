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
        public string ea_application_code { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> handover_date { get; set; }
        public string handover { get; set; }
        public bool is_confirmed_handover { get; set; }
        public Nullable<System.DateTime> receipt_date { get; set; }
        public string receipter { get; set; }
        public bool is_confirmed_recipienter { get; set; }
        public string handover_note { get; set; }
    }
}