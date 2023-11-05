using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class AllocationEquipmentsController : Controller
    {
        // GET: AllocationEquipments
        public ActionResult CreateNewAllocationEquipmentsRequest()
        {
            return View();
        }
        public ActionResult AllocationEquipmentsRequestsList()
        {
            return View();
        }
        public ActionResult AllocationEquipmentRequestDetails()
        {
            return View();
        }
        public ActionResult AllocationEquipmentsRequestsListPV()
        {
            return PartialView();
        }
    }
}