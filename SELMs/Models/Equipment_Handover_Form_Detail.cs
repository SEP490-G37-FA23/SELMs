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
    
    public partial class Equipment_Handover_Form_Detail
    {
        public int form_detail_id { get; set; }
        public string form_code { get; set; }
        public Nullable<int> application_detail_id { get; set; }
        public string system_equipment_code { get; set; }
        public string equipment_name { get; set; }
        public string equipment_specification { get; set; }
        public string unit { get; set; }
        public string usage_status { get; set; }
        public string note { get; set; }
    
        public virtual Equipment_Handover_Form Equipment_Handover_Form { get; set; }
    }
}
