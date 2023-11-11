using System;

namespace SELMs.Api.DTOs
{
	public class EquipmentProjectHistoryDTO
	{
		public int? project_id { get; set; }
		public string system_equiment_code { get; set; }
		public DateTime? from_date { get; set; }
		public DateTime? to_date { get; set; }
		public string note { get; set; }
	}
}