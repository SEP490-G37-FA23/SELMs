using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SELMs.Models.BusinessModel
{
    public static class HandleDateTime
    {
        public static DateTime formatDate(string date)
        {
            try
            {
                date = date.Trim();
                DateTime date1;
                if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }
                else if (DateTime.TryParseExact(date, "dd/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }
                else if (DateTime.TryParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }
                else if (DateTime.TryParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }
                else if (DateTime.TryParseExact(date, "M/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }

                else if (DateTime.TryParseExact(date, "M/d/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }

                else if (DateTime.TryParseExact(date, "M/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }

                else if (DateTime.TryParseExact(date, "MM/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }

                else if (DateTime.TryParseExact(date, "MM/d/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }


                else if (DateTime.TryParseExact(date, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }

                else if (DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                {
                    return date1;
                }
                else
                {
                    throw new Exception(date + " không đúng định dạng ngày/tháng/năm , vui lòng kiểm tra lại!");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Input string was not in a correct format.")
                {
                    throw new Exception("Date => Content change không đúng định dạng dd/MM/yyyy , vui lòng kiểm tra lại!");
                }
                else
                {
                    throw ex;
                }

            }

        }
    }
}