using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SELMs.Controllers
{
    public class ProjectPortfolioController : Controller
    {
        // GET: ProjectPortfolio
        public ActionResult ViewProjectList()
        {
            return View();
        }

        public ActionResult CreateNewProject()
        {
            return View();
        }

        public ActionResult ViewProjectDetails() 
        {
            return View();
        }

        public ActionResult ViewProjectMember() 
        {
            return View();
        }

        public ActionResult ViewProjectEquipments()
        {
            return View();
        }

        public ActionResult ViewProjectListPV() 
        { 
            return PartialView();
        }
    }
}