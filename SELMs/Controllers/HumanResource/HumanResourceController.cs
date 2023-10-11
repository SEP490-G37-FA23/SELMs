using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.HumanResource
{
    public class HumanResourceController : Controller
    {
        // GET: HumanResource
        public ActionResult MembersList()
        {
            return View();
        }
    }
}