using System.Collections.Generic;
using Newtonsoft.Json;

namespace SELMs.Api.DTOs
{
	public class EquipmentHandoverFormDTO
	{
		public int form_id { get; set; }
		public string form_code { get; set; }
		public string create_date { get; set; }
		public string creater { get; set; }
		public string description { get; set; }
		public string handover_date { get; set; }
		public string handover { get; set; }
		public string receipt_date { get; set; }
		public string receipter { get; set; }
		public string handover_note { get; set; }
		public bool is_finish { get; set; }
		public int location_id { get; set; }
		public int project_id { get; set; }
	}


	public class EquipmentHandoverFormDTO2 : EquipmentHandoverFormDTO
	{
		[JsonProperty("receipter_name")]
		public new string receipter { get; set; }

		[JsonProperty("location_description")]
		public new string description { get; set; }

		[JsonProperty("handover_name")]
		public new string handover { get; set; }
		public string project_name { get; set; }
		public string location_code { get; set; }
		public List<EquipmentHandoverFormDetailDTO> Equipment_Handover_Form_Detail { get; set; }
	}
}