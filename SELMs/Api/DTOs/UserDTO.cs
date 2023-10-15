using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class UserDTO
    {
        public int? user_id { get; set; }
        public string member_code { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string position_code { get; set; }
        public bool? is_admin { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string hotline { get; set; }
        public string email { get; set; }
        public bool? gender { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
        public DateTime? hire_date { get; set; }
        public DateTime? resignation_date { get; set; }
        public string work_term { get; set; }
        public string skill { get; set; }
        public string job_description { get; set; }
        public bool active { get; set; }
    }
}