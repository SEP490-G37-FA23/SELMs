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
    
    public partial class Image
    {
        public int image_id { get; set; }
        public string image_name { get; set; }
        public Nullable<int> equipment_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public byte[] content { get; set; }
    
        public virtual Equipment Equipment { get; set; }
    }
}
