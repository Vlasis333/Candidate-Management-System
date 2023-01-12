using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assigment3B.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminsUI()
        {
            //ViewBag.Message = "All capabilities of the Admin.";
            return View();
        }

        public ActionResult CandidatesUI()
        {
            //ViewBag.Message = "All capabilities of the Candidate.";
            return View();
        }
    }
}