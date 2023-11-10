using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class EquipmentImportController : Controller
    {
        // GET: ImportEquipments
        public ActionResult CreateNewEIA()
        {
            return View();
        }
        public ActionResult EIAList()
        {
            return View();
        }

        public ActionResult EIADetail()
        {
            return View();
        }

    }
}