using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_QLBH_BQD.Controllers.HumanResourceManagement
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