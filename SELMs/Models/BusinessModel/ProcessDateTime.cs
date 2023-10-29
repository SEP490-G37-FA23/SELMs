using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SELMs.Models.BusinessModel
{
    public class ProcessDateTime
    {
        public DateTime Processdatetime(string date)
        {
            int ngay = Convert.ToInt32(date.Substring(0, 2));
            int thang = Convert.ToInt32(date.Substring(3, 2));

            int nam = Convert.ToInt32(date.Substring(6, 4));
            DateTime dt = new DateTime(nam, thang, ngay);
            return dt;
        }

        public DateTime Processdatetime2(string date)
        {
            int nam = Convert.ToInt32(date.Substring(0, 4));
            int thang = Convert.ToInt32(date.Substring(5, 2));
            int ngay = Convert.ToInt32(date.Substring(8, 2));

            DateTime dt = new DateTime(nam, thang, ngay);
            return dt;
        }

     

        public static DateTime ConvertToTime(string time)
        {
            string[] timesplit = time.Split('/');
            return new DateTime(Convert.ToInt32(timesplit[2]), Convert.ToInt32(timesplit[1]), Convert.ToInt32(timesplit[0]));
        }
    }
}