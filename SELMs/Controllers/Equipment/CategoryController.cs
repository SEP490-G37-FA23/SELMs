using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class CategoryController : Controller
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
        public ActionResult CategoryDetail()
        {
            return View();
        }
    }
}