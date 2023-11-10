using System;

namespace SELMs.Api.DTOs
{
	public class MemberProjectHistoryDTO
	{
		public int? project_id { get; set; }
		public string user_code { get; set; }
		public string status { get; set; }
		public string note { get; set; }
		public DateTime? date { get; set; }
	}
}