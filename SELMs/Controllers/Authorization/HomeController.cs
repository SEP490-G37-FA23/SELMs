﻿
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
            Session["isadmin"] = "";
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            ViewBag.Title = "Login Page";

            return View();
        }

        public class ThamSo
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        private SELMsContext db = new SELMsContext();
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            var user = db.Users.SingleOrDefault(x => x.username == username && x.password == password && x.is_active == true);
            if (user != null)
            {
                Session["isadmin"] = user.is_admin;
                Session["username"] = user.username;
                Session["fullname"] = user.fullname;
                Session["role"] = user.role_code;
                switch (user.role_code)
                {
                    case "HR":
                        return RedirectToAction("MembersList", "User", new { area = "" });
                        break;
                    case "LD":
                        return RedirectToAction("LocationsList", "Locations", new { area = "" });
                        break;
                    case "SM":
                        return RedirectToAction("EquipmentsList", "Equipments", new { area = "" });
                        break;
                    case "PM":
                        return RedirectToAction("ViewProjectList", "ProjectPortfolio", new { area = "" });
                        break;
                    case "MB":
                        return RedirectToAction("LocationsList", "Locations", new { area = "" });
                        break;
                }
                
                
               
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
