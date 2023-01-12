using EFDataAccess.Data;
using EFDataAccess.Data.Repositories;
using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebApplication.Controllers
{
    public class CandidateCertificatesController : Controller
    {
        private readonly ICandidateRepository _candidateRepository;
        private static int _candidateId; // so it will not get disposed

        // GET: CandidateCertificates
        public CandidateCertificatesController()
        {
            _candidateRepository = new CandidateRepository(new AppDBContext());
        }

        public CandidateCertificatesController(ICandidateRepository certificateRepository)
        {
            _candidateRepository = certificateRepository;
        }

        /// <summary>
        /// Used to get the Id enterd by the user and log in as this current candidate
        /// </summary>
        [HttpPost]
        public ActionResult Index(int candidateId)
        {
            var currentCandidate = _candidateRepository.GetCandidate(candidateId);

            if (currentCandidate != null)
            {
                ViewBag.Title = $"Logged in as {currentCandidate.FirstName} {currentCandidate.LastName}";
                ViewBag.Message = "My Certificates";

                _candidateId = candidateId;

                return View(_candidateRepository.GetCertificatesByCandidateId(candidateId));
            }
            else
            {
                ViewBag.Message = "This Id does not exist, please provide us with the correct Candidate Number (Id):";
                return View("~/Views/Home/CandidateUI.cshtml");
            }
        }

        // <summary>
        // Download all the certificates of the current candidate
        // </summary>            
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DownloadAllCertificates()
        {
            string pdfPath = _candidateRepository.DownloadAllCertificatesOfCandidate(_candidateId);

            if (!pdfPath.StartsWith("Error"))
            {
                byte[] FileBytes = System.IO.File.ReadAllBytes(pdfPath);
                return File(FileBytes, "application/pdf");
            }
            else
            {
                return Content($"<script language='javascript' type='text/javascript'>alert('{pdfPath}');</script>");
            }
        }

        // <summary>
        // Download the selected certificate of the current candidate
        // </summary>            
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult DownloadSelectedCertificate()
        //{
        //    string s = _candidateRepository.DownloadAllCertificatesOfCandidate(_candidateId);

        //    if (!s.StartsWith("Error"))
        //    {
        //        string ReportURL = s;
        //        byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
        //        return File(FileBytes, "application/pdf");
        //    }
        //    else
        //    {
        //        return Content($"<script language='javascript' type='text/javascript'>alert('{s}');</script>");
        //    }
        //}
    }
}