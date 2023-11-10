using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentAllocationApplicationRequest
    {
        public EquipmentAllocationApplicationDTO application { get; set; }
        public List<EquipmentAllocationApplicationDetailDTO> application_details { get; set; }
    }
}