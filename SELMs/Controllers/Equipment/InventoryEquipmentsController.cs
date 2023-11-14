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

        public ActionResult ListEquipmentsAlreadyExist()
        {
            return View();
        }
        public ActionResult _CreateNewInventoryEquipmentsRequest()
        {
            return PartialView();
        }

        public ActionResult ListEquipmentNotExist()
        {
            return View();
        }

        public ActionResult _CreateNewImportEquipmentsProposal()
        {
            return PartialView();
        }
    }
}