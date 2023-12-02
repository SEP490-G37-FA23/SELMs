using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class InventoryRequestRequest
    {
        public InventoryRequestApplicationDTO application { get; set; }
        public List<InventoryRequestApplicationDetailDTO> application_details { get; set; }
    }
}