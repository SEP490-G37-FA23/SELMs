
using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers
{
    public class HomeController : Controller
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

            var user = db.Users.SingleOrDefault(x => x.username == username && x.password == password);
            if (user != null)
            {
                Session["isadmin"] = user.is_admin;
                Session["username"] = user.username;
                Session["fullname"] = user.fullname;
                Session["position"] = user.position_code;



                return RedirectToAction("MembersList", "HumanResource", new { area = "" });
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
