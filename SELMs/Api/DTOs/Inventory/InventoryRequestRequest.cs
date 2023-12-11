using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class InventoryRequestRequest
    {
        public InventoryRequestApplicationDTO application { get; set; }
        public List<string> application_details { get; set; }
    }
}