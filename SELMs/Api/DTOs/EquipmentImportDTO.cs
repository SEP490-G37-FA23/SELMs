using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class EquipmentImportDTO
    {
        public string username {  get; set; }
        public List<EquipmentDTO> ListEquipImport { get; set; }
    }
}