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
    
    public partial class Member_Project_History
    {
        public int id { get; set; }
        public Nullable<int> project_id { get; set; }
        public string user_code { get; set; }
        public string status { get; set; }
        public string note { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    }
}
