using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class ImageRequest
    {
        public string name {  get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}