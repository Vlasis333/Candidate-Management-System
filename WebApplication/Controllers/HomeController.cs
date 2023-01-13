using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDataAccess;
using EFDataAccess.Data;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome visitor, please select an option.";

            return View();
        }

        public ActionResult CandidateLogIn()
        {
            ViewBag.Message = "Please provide us with your Candidate Number:";

            return View();
        }
    }
}