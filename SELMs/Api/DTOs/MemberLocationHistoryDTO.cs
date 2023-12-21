using System;
using System.Collections.Generic;

namespace SELMs.Api.DTOs
{
	public class MemberLocationHistoryDTO
	{
		public string user_code { get; set; }
		public int? location_id { get; set; }
		public string reason { get; set; }
		public DateTime? date { get; set; }
	}

	public class ListMemberLocationHistoryDTO
	{
		public List<MemberLocationHistoryDTO> ListMembersJoinLocation;
    }
}
