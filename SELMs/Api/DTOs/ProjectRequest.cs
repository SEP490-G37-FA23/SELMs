using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class ProjectRequest
    {
        public ProjectDTO Project { get; set; }
        public List<string> ProjectEquipments { get; set; }
        public List<string> ProjectMembers { get; set; }
    }
}