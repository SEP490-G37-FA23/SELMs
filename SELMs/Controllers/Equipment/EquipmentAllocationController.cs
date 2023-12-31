﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class EquipmentAllocationController : Controller
    {
        // GET: AllocationEquipments
        public ActionResult CreateNewEAA()
        {
            return View();
        }
        public ActionResult EAAList()
        {
            return View();
        }
        public ActionResult EAADetails()
        {
            return View();
        }
        public ActionResult EquipAllocationDetails()
        {
            return View();
        }
        public ActionResult DeliveryEquipmentsList()
        {
            return View();
        }
        public ActionResult _CreateNewHandoverEquipmentsRequest()
        {
            return PartialView();
        }
        public ActionResult _CreateNewImportEquipmentsProposal()
        {
            return PartialView();
        }
        public ActionResult HEFList()
        {
            return View();
        }
    }
}