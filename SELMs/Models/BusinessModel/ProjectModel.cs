﻿using System.Collections.Generic;

namespace SELMs.Models.BusinessModel
{
	public class ProjectModel
	{
		public Project Project { get; set; }
		public List<Equipment> ProjectEquipments { get; set; }
		public List<User> ProjectMembers { get; set; }
	}

	public class ProjectDetailModel
	{
		public Project Project { get; set; }
		public List<string> ProjectEquipments { get; set; }
		public List<string> ProjectMembers { get; set; }
	}
}