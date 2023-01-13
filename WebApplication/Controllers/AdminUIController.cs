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
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Logged in as Administrator";

            return View(await _adminrepository.GetAllCandidates());
        }

        /// <summary>
        /// Details of the selected candidate
        /// </summary>
        public async Task<ActionResult> Details(int id)
        {
            ViewBag.Title = "Logged in as Administrator";

            var currentCandidate = await _adminrepository.GetCandidate(id);

            ViewBag.Message = $"Details of {currentCandidate.FirstName} {currentCandidate.LastName}";

            return await Task.Run(() => View(currentCandidate));
        }

        public async Task<ActionResult> Create()
        {
            //default create
            ViewBag.Title = "Logged in as Administrator";
            ViewBag.Message = "Create New Candidate";

            await PopulatedDropDownLists();

            return await Task.Run(() => View());
        }

        /// <summary>
        /// Create new candidate from view
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Logged in as Administrator";
                ViewBag.Message = "Create New Candidate";

                await _adminrepository.AddCandidate(candidate);
                return await Task.Run(() => RedirectToAction("Index"));
            }

            await PopulatedDropDownLists();

            return await Task.Run(() => View(candidate));
        }

        public async Task<ActionResult> Edit(int id)
        {
            //default edit
            ViewBag.Title = "Logged in as Administrator";

            var currentCandidate = await _adminrepository.GetCandidate(id);

            ViewBag.Message = $"Edit candidate: {currentCandidate.FirstName} {currentCandidate.LastName}";

            await PopulatedDropDownLists(currentCandidate);

            return await Task.Run(() => View(currentCandidate));
        }

        /// <summary>
        /// Edit and save current edited candidate
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Logged in as Administrator";
                ViewBag.Message = $"Edit candidate: {candidate.FirstName} {candidate.LastName}";

                await _adminrepository.UpdateCandidate(candidate);

                return await Task.Run(() => RedirectToAction("Index"));
            }

            await PopulatedDropDownLists(candidate);

            return await Task.Run(() => View(candidate));
        }

        /// <summary>
        /// Form for the selected candidate to be deleted
        /// </summary>
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.Title = "Logged in as Administrator";

            var currentCandidate = await _adminrepository.GetCandidate(id);

            ViewBag.Message = $"Are you sure you want to delete {currentCandidate.FirstName} {currentCandidate.LastName}?";

            return await Task.Run(() => View(currentCandidate));
        }

        /// <summary>
        /// Confirms the delete of a candidate
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _adminrepository.DeleteCandidate(id);
            return await Task.Run(() => RedirectToAction("Index"));
        }

        /// <summary>
        /// Display every certificate (passed or not) for every candidate
        /// </summary>
        public async Task<ActionResult> ShowAllCertificates()
        {
            ViewBag.Title = "Logged in as Administrator";
            ViewBag.Message = "List of all certificates for each candidate";

            return View(await _adminrepository.GetAllCandidatesWithCertificates());
        }

        /// <summary>
        /// Create a VewBag element to be used for the photo identification list
        /// </summary>
        private async Task AddDropDownPhotoIdentifications(Candidate candidate = null)
        {
            var listOfIdentifications = new List<SelectListItem>();
            var identificationTypes = await _adminrepository.GetAllPhotoIdentifications();

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
        private async Task PopulatedDropDownLists(Candidate candidate = null)
        {
            await AddDropDownPhotoIdentifications(candidate);
            AddDropDown(typeof(LanguagesEnum), candidate);
            AddDropDown(typeof(GendersEnum), candidate);
        }
    }
}