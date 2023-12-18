using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentHandoverRequest
    {
        public EquipmentHandoverFormDTO application { get; set; }
        public List<EquipmentHandoverFormDetailDTO> application_details { get; set; }
    }
}