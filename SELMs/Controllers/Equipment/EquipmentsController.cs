using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class EquipmentsController : Controller
    {
        // GET: Equipments
        public ActionResult EquipmentsList()
        {
            return View();
        }
        public ActionResult CreateNewEquipment()
        {
            return View();
        }
        public ActionResult EquipmentDetails()
        {
            return View();
        }
        public ActionResult EquipmentsListPV()
        {
            return PartialView();
        }
        public ActionResult EquipmentsGenderQR()
        {
            return View();
        }
    }
}