using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_QLBH_BQD.Controllers.Equipment_Management
{
    public class EquipmentsController : Controller
    {
        // GET: Equipments
        public ActionResult EquipmentsList()
        {
            return View();
        }
    }
}