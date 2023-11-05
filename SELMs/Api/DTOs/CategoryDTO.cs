using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class CategoryDTO
    {
        public int? category_id { get; set; }
        public string category_code { get; set; }
        public string category_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public int? category_level { get; set; }
        public int? category_parent_id { get; set; }
    }
}