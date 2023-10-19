using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SELMs.Models.BusinessModel
{
    public class Argument
    {
        public int id { set; get; }
        public int group_id { set; get; }

        public string fullname { set; get; }
        public string username { set; get; }
        public string memberCode { set; get; }

    }
}