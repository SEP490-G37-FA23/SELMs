using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_QLBH_BQD.Controllers.EquipmentManagement
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