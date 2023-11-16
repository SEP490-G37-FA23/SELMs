using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SELMs.Models.BusinessModel
{
    public class Argument
    {
        public int id { set; get; }
        public int id1 { set; get; }
        public int id2 { set; get; }
        public int level { set; get; }
        public int group_id { set; get; }

        public string fullname { set; get; }
        public string username { set; get; }
        public string memberCode { set; get; }
        public string categoryCode { set; get; }

        public bool isadmin { set; get; }
        public string role { set; get; }
        public string text { set; get; }
        public string text1 { set; get; }
        public string text2 { set; get; }
        public string text3 { set; get; }

    }
}