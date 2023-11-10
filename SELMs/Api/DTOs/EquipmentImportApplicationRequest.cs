using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentImportApplicationRequest
    {
        public EquipmentImportApplicationDTO application { get; set; }
        public List<EquipmentImportApplicationDetailDTO> application_details { get; set; }
    }
}