using EFDataAccess.Data;
using EFDataAccess.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class CertificatesManagerController : Controller
    {
        private readonly ICertificateManagerRepository _certificateManagerRepository;

        public CertificatesManagerController()
        {
            _certificateManagerRepository = new CertificateManagerRepository(new AppDBContext());
        }

        public CertificatesManagerController(ICertificateManagerRepository certificateManagerRepository)
        {
            _certificateManagerRepository = certificateManagerRepository;
        }

        // GET: CertificatesManager
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Logged in as Certificate Manager";

            return View(await _certificateManagerRepository.GetCertificates());
        }
    }
}