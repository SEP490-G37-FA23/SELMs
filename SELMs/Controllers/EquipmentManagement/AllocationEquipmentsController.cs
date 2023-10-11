using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_QLBH_BQD.Controllers.EquipmentManagement
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
        public ActionResult DeliveryEquipmentsList()
        {
            return View();
        }
    }
}