using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.HumanResource
{
    public class UserController : Controller
    {
        // GET: HumanResource
        public ActionResult MembersList()
        {
            return View();
        }

        //Get: NewMember
        [HttpGet]
        public ActionResult AddNewMember()
        {
            return View();
        }

        public ActionResult MemberDetails()
        {
            return PartialView();
        }

        public ActionResult MemberListPV()
        {
            return PartialView();
        }
    }
}