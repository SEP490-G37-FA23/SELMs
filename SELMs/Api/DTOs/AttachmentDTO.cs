using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class AttachmentDTO
    {
        public int attach_id { get; set; }
        public string name { get; set; }
        public byte[] content { get; set; }
        //public Nullable<System.DateTime> date { get; set; }
        public string date { get; set; }
        public string notes { get; set; }
        public string extension { get; set; }
    }
}