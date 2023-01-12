using EFDataAccess.Data;
using EFDataAccess.Data.Repositories;
using EFDataAccess.Helpers;
using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class AdminUIController : Controller
    {
        private readonly IAdminRepository _adminrepository;

        public AdminUIController()
        {
            _adminrepository = new AdminRepository(new AppDBContext());
        }

        public AdminUIController(IAdminRepository adminrepository)
        {
            _adminrepository = adminrepository;
        }

        // GET: AdminUI (list of all the candidates)
        public ActionResult Index()
        {
            ViewBag.Title = "Logged in as Administrator";

            return View(_adminrepository.GetAllCandidates());
        }

        /// <summary>
        /// Details of the selected candidate
        /// </summary>
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Logged in as Administrator";

            var currentCandidate = _adminrepository.GetCandidate(id);

            ViewBag.Message = $"Details of {currentCandidate.FirstName} {currentCandidate.LastName}";

            return View(currentCandidate);
        }

        public ActionResult Create()
        {
            //default create
            ViewBag.Title = "Logged in as Administrator";
            ViewBag.Message = "Create New Candidate";

            PopulatedDropDownLists();

            return View();
        }

        /// <summary>
        /// Create new candidate from view
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Logged in as Administrator";
                ViewBag.Message = "Create New Candidate";

                _adminrepository.AddCandidate(candidate);
                return RedirectToAction("Index");
            }

            PopulatedDropDownLists();

            return View(candidate);
        }

        public ActionResult Edit(int id)
        {
            //default edit
            ViewBag.Title = "Logged in as Administrator";

            var currentCandidate = _adminrepository.GetCandidate(id);

            ViewBag.Message = $"Edit candidate: {currentCandidate.FirstName} {currentCandidate.LastName}";

            PopulatedDropDownLists(currentCandidate);

            return View(currentCandidate);
        }

        /// <summary>
        /// Edit and save current edited candidate
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Logged in as Administrator";
                ViewBag.Message = $"Edit candidate: {candidate.FirstName} {candidate.LastName}";

                _adminrepository.UpdateCandidate(candidate);

                return RedirectToAction("Index");
            }

            PopulatedDropDownLists(candidate);

            return View(candidate);
        }

        /// <summary>
        /// Form for the selected candidate to be deleted
        /// </summary>
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Logged in as Administrator";

            var currentCandidate = _adminrepository.GetCandidate(id);

            ViewBag.Message = $"Are you sure you want to delete {currentCandidate.FirstName} {currentCandidate.LastName}?";

            return View(currentCandidate);
        }

        /// <summary>
        /// Confirms the delete of a candidate
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _adminrepository.DeleteCandidate(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Display every certificate (passed or not) for every candidate
        /// </summary>
        public ActionResult ShowAllCertificates()
        {
            ViewBag.Title = "Logged in as Administrator";
            ViewBag.Message = "List of all certificates for each candidate";

            return View(_adminrepository.GetAllCandidatesWithCertificates());
        }

        /// <summary>
        /// Create a VewBag element to be used for the photo identification list
        /// </summary>
        private void AddDropDownPhotoIdentifications(Candidate candidate = null)
        {
            var listOfIdentifications = new List<SelectListItem>();
            var identificationTypes = _adminrepository.GetAllPhotoIdentifications();

            var group = new SelectListGroup();

            foreach (var identificationType in identificationTypes)
            {
                var selectListItem = new SelectListItem()
                {
                    Disabled = false,
                    Group = group,
                    Selected = false,
                    Text = identificationType.Type,
                    Value = identificationType.PhotoIdentificationTypeId.ToString()
                };

                if (candidate != null)
                {
                    if (identificationType == candidate.CandidatePhotoIdentification.PhotoIdentificationType)
                    {
                        selectListItem.Selected = true;
                    }
                }

                listOfIdentifications.Add(selectListItem);
            }
            ViewBag.IdentificationsItems = new SelectList(listOfIdentifications, "Value", "Text");
        }

        /// <summary>
        /// Dynamic method that can create a drop down list based on the given enum type provided
        /// </summary>
        private void AddDropDown(Type enumType, Candidate candidate = null)
        {
            var list = new List<SelectListItem>();

            var group = new SelectListGroup();

            foreach (string item in Enum.GetNames(enumType))
            {
                var selectListItem = new SelectListItem()
                {
                    Disabled = false,
                    Group = group,
                    Selected = false,
                    Text = item,
                    Value = item
                };

                if (candidate != null)
                {
                    switch (enumType.Name)
                    {
                        case "LanguagesEnum":
                            if (item == candidate.NativeLanguage)
                            {
                                selectListItem.Selected = true;
                            }
                            break;
                        case "GendersEnum":
                            if (item == candidate.Gender)
                            {
                                selectListItem.Selected = true;
                            }
                            break;
                    }
                }

                list.Add(selectListItem);
            }

            switch (enumType.Name)
            {
                case "LanguagesEnum":
                    ViewBag.LanguageItems = new SelectList(list, "Value", "Text");
                    break;
                case "GendersEnum":
                    ViewBag.GenderItems = new SelectList(list, "Value", "Text");
                    break;
            }
        }

        /// <summary>
        /// Method Used to populate all the drop down editors on our views
        /// </summary>
        private void PopulatedDropDownLists(Candidate candidate = null)
        {
            AddDropDownPhotoIdentifications(candidate);
            AddDropDown(typeof(LanguagesEnum), candidate);
            AddDropDown(typeof(GendersEnum), candidate);
        }
    }
}