using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class LocationDTO
    {
        public int? location_id { get; set; }
        public string location_code { get; set; }
        public string location_desciption { get; set; }
        public int? parent_location_id { get; set; }
        public int? location_level { get; set; }
        public bool is_active { get; set; }
        public int number_equip { get; set; }
    }

    public class DetailLocationDTO
    {
        public Location location_info { get; set; }
        public List<LocationDTO> ListSubLocation { get; set; }
        public List<ProjectDTO> ListProjectInLocation { get; set; }
        public List<EquipmentDTO> ListEquipmentInLocation { get; set; }
        public List<UserDTO> ListMemberInLocation { get; set; }
    }
}