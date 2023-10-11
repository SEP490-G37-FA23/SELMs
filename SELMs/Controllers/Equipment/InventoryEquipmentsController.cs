using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class InventoryEquipmentsController : Controller
    {
        // GET: InventoryEquipments
        public ActionResult CreateNewInventoryEquipmentsRequest()
        {
            return View();
        }
        public ActionResult PerformInventoryEquipmentsRequest()
        {
            return View();
        }
    }
}