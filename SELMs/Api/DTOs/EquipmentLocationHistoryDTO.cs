using System;

namespace SELMs.Api.DTOs
{
	public class EquipmentLocationHistoryDTO
	{
		public int location_id { get; set; }
		public string system_equipment_code { get; set; }
		public DateTime? from_date { get; set; }
		public DateTime? to_date { get; set; }
		public string note { get; set; }
	}
}