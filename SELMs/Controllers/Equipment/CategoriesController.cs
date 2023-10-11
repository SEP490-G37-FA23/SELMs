using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult CreateNewCategory()
        {
            return View();
        }

        public ActionResult CategoriesList()
        {
            return View();
        }
    }
}