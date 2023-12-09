using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentRequest
    {
        public EquipmentDTO equipment { get; set; }
        public List<HttpPostedFileBase> images { get; set; }
    }
}