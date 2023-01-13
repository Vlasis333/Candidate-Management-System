using EFDataAccess.Data;
using EFDataAccess.Data.Repositories;
using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index(int candidateId)
        {
            var currentCandidate = await _candidateRepository.GetCandidate(candidateId);

            if (currentCandidate != null)
            {
                ViewBag.Title = $"Logged in as {currentCandidate.FirstName} {currentCandidate.LastName}";
                ViewBag.Message = "Candidate's UI";

                _candidateId = candidateId;

                return View(await _candidateRepository.GetCertificatesOfCandidate(candidateId));
            }
            else
            {
                ViewBag.Message = "This Id does not exist, please provide us with the correct Candidate Number (Id):";
                return await Task.Run(() => View("~/Views/Home/CandidateUI.cshtml"));
            }
        }

        // <summary>
        // Download all the certificates of the current candidate
        // </summary>            
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> DownloadAllCertificates()
        {
            string returnedString = await _candidateRepository.DownloadAllCertificatesOfCandidate(_candidateId);

            if (!returnedString.StartsWith("Error"))
            {
                byte[] FileBytes = System.IO.File.ReadAllBytes(returnedString);
                return await Task.Run(() => File(FileBytes, "application/pdf"));
            }
            else
            {
                return await Task.Run(() => Content($"<script language='javascript' type='text/javascript'>alert('{returnedString}');</script>"));
            }
        }

        // <summary>
        // Download the selected certificate of the current candidate
        // </summary>            
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> DownloadSelectedCertificate(int certificateId)
        {
            string returnedString = await _candidateRepository.DownloadSelectedCertificateOfCandidate(_candidateId, certificateId);

            if (!returnedString.StartsWith("Error"))
            {
                string ReportURL = returnedString;
                byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
                return await Task.Run(() => File(FileBytes, "application/pdf"));
            }
            else
            {
                return await Task.Run(() => Content($"<script language='javascript' type='text/javascript'>alert('{returnedString}');</script>"));
            }
        }
    }
}