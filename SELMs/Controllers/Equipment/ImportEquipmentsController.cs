using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers.Equipment
{
    public class ImportEquipmentsController : Controller
    {
        // GET: ImportEquipments
        public ActionResult CreateNewImportEquipmentsProposal()
        {
            return View();
        }
        public ActionResult ImportEquipmentsProposalsList()
        {
            return View();
        }
    }
}