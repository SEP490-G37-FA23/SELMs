using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class UserDTO
    {
        public int? user_id { get; set; }
        public string user_code { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string role_code { get; set; }
        public Nullable<bool> is_admin { get; set; }
        //public Nullable<System.DateTime> date_of_birth { get; set; }
        public string date_of_birth { get; set; }
        public string hotline { get; set; }
        public string email { get; set; }
        public Nullable<bool> gender { get; set; }
        public string address { get; set; }
        public Nullable<int> avatar_img { get; set; }
        //public Nullable<System.DateTime> hire_date { get; set; }
        public string hire_date { get; set; }
        //public Nullable<System.DateTime> resignation_date { get; set; }
        public string resignation_date { get; set; }
        public string work_term { get; set; }
        public string skill { get; set; }
        public string job_description { get; set; }
        public Nullable<bool> is_active { get; set; }
    }
}