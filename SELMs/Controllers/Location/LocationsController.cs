using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Location
{
    public class LocationsController : Controller
    {
        public ActionResult LocationsList()
        {
            return View();
        }
        public ActionResult LocationDetails()
        {
            return View();
        }
    }
}