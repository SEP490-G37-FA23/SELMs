using ERP_QLBH_BQD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";

            return View();
        }

        public class ThamSo
        {
            public string username { get; set; }
            public string password { get; set; }
        }

       private SELMsEntities db = new SELMsEntities();
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            var user = db.SYSTEM_USERS.SingleOrDefault(x => x.USER_NAME == username && x.PASSWORD == password);
            if (user != null)
            {
                Session["ISADMIN"] = user.IS_ADMIN;
                Session["USERNAME"] = user.USER_NAME;
                Session["FULL_NAME"] = user.FULL_NAME;


                return RedirectToAction("MemberList", "MemberManagement", new { area = "" });
            }
            ViewBag.error = "Bạn đã đăng nhập sai";
            return View();
        }
        public ActionResult Logout()
        {

            Session["USERNAME"] = null;
            Session["PASSWORD"] = null;

            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
