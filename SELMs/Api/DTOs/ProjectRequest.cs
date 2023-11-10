using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class ProjectRequest
    {
        public ProjectDTO Project { get; set; }
        public List<EquipmentDTO> ProjectEquipments { get; set; }
        public List<UserDTO> ProjectMembers { get; set; }
    }
}