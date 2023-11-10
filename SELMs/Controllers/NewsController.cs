using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult NotificationList()
        {
            return View();
        }

        public ActionResult CreateNewNotification()
        {
            return View();
        }
        public ActionResult NotificationDetail()
        {
            return View();
        }



    }
}
