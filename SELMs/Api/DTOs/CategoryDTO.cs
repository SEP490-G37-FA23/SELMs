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
    }
}