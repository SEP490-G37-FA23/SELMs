using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class ImageDTO
    {
        public int image_id { get; set; }
        public string image_name { get; set; }
        public byte[] content { get; set; }
        public Nullable<int> equipment_id { get; set; }
        //public Nullable<System.DateTime> date { get; set; }
        public string date { get; set; }
    }
}