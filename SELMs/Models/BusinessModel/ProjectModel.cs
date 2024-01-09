using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Models.BusinessModel
{
    public class ProjectModel
    {
        public dynamic Project { get; set; }
        public List<Equipment> ProjectEquipments { get; set; }
        public List<User> ProjectMembers { get; set; }
    }
}