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
    
    public partial class Location
    {
        public int location_id { get; set; }
        public string location_code { get; set; }
        public string location_desciption { get; set; }
        public Nullable<int> parent_location_id { get; set; }
        public Nullable<int> location_level { get; set; }
        public bool is_active { get; set; }
    }
}
