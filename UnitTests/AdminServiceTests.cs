using EFDataAccess.Data;
using EFDataAccess.Data.Repositories;
using EFDataAccess.Helpers;
using EFDataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CRUDTests
    {
        private readonly IAdminRepository _adminrepository = new AdminRepository(new AppDBContext());

        /// <summary>
        /// Verifies that a new candidate can be added to the database
        /// </summary>
        [TestMethod]
        public async Task TestAddCandidate()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "First",
                MiddleName = "Middle",
                LastName = "Last",
                Gender = GendersEnum.Male.ToString(),
                NativeLanguage = LanguagesEnum.Greek.ToString(),
                BirthDate = new DateTime(2020, 11, 6),
                CandidateLocation = new CandidateLocation
                {
                    Address = "Street",
                    Address2 = "Street 2",
                    City = "RandomCity",
                    Province = "Test",
                    Residence = "Resndom",
                    PostalCode = "12345"
                },
                CandidateContact = new CandidateContact
                {
                    MobileNumber = "6971234567",
                    LandlineNumber = "2109876543",
                    Email = "test@testing.com"
                },
                CandidatePhotoIdentification = new CandidatePhotoIdentification
                {
                    Number = "123",
                    IssueDate = new DateTime(2015, 1, 1),
                    PhotoIdentificationId = 1,
                }
            };

            // Act
            await _adminrepository.AddCandidate(candidate);

            // Assert
            var savedCandidate = await _adminrepository.GetCandidate(candidate.CandidateId);
            Assert.AreEqual(candidate.FirstName, savedCandidate.FirstName);
            Assert.AreEqual(candidate.MiddleName, savedCandidate.MiddleName);
            Assert.AreEqual(candidate.LastName, savedCandidate.LastName);
            Assert.AreEqual(candidate.Gender, savedCandidate.Gender);
            Assert.AreEqual(candidate.NativeLanguage, savedCandidate.NativeLanguage);
            Assert.AreEqual(candidate.BirthDate, savedCandidate.BirthDate);
            Assert.AreEqual(candidate.CandidateLocation.Address, savedCandidate.CandidateLocation.Address);
            Assert.AreEqual(candidate.CandidateLocation.Address2, savedCandidate.CandidateLocation.Address2);
            Assert.AreEqual(candidate.CandidateLocation.City, savedCandidate.CandidateLocation.City);
            Assert.AreEqual(candidate.CandidateLocation.Province, savedCandidate.CandidateLocation.Province);
            Assert.AreEqual(candidate.CandidateLocation.Residence, savedCandidate.CandidateLocation.Residence);
            Assert.AreEqual(candidate.CandidateLocation.PostalCode, savedCandidate.CandidateLocation.PostalCode);
            Assert.AreEqual(candidate.CandidateContact.MobileNumber, savedCandidate.CandidateContact.MobileNumber);
            Assert.AreEqual(candidate.CandidateContact.LandlineNumber, savedCandidate.CandidateContact.LandlineNumber);
            Assert.AreEqual(candidate.CandidateContact.Email, savedCandidate.CandidateContact.Email);
            Assert.AreEqual(candidate.CandidatePhotoIdentification.Number, savedCandidate.CandidatePhotoIdentification.Number);
            Assert.AreEqual(candidate.CandidatePhotoIdentification.PhotoIdentificationId, savedCandidate.CandidatePhotoIdentification.PhotoIdentificationId);
        }

        /// <summary>
        /// Verifies that a candidate can be retrieved from the database
        /// </summary>
        [TestMethod]
        public async Task TestGetCandidate()
        {
            // Arrange
            var candidateId = 2;

            // Act
            var candidate = await _adminrepository.GetCandidate(candidateId);

            // Assert
            Assert.IsNotNull(candidate);
            Assert.AreEqual("Ina", candidate.FirstName);
            Assert.AreEqual("", candidate.MiddleName);
            Assert.AreEqual("Bogdani", candidate.LastName);
            Assert.AreEqual(GendersEnum.Female.ToString(), candidate.Gender);
            Assert.AreEqual(LanguagesEnum.Albanian.ToString(), candidate.NativeLanguage);
            Assert.AreEqual(new DateTime(1997, 6, 4), candidate.BirthDate);
            Assert.AreEqual("Eget Felis", candidate.CandidateLocation.Address);
            Assert.AreEqual(null, candidate.CandidateLocation.Address2);
            Assert.AreEqual("Fusce", candidate.CandidateLocation.City);
            Assert.AreEqual("Nam", candidate.CandidateLocation.Province);
            Assert.AreEqual("Suspendisse", candidate.CandidateLocation.Residence);
            Assert.AreEqual("12345", candidate.CandidateLocation.PostalCode);
            Assert.AreEqual("1947274863", candidate.CandidateContact.MobileNumber);
            Assert.AreEqual("210-1234567", candidate.CandidateContact.LandlineNumber);
            Assert.AreEqual("ina@gmail.com", candidate.CandidateContact.Email);
            Assert.AreEqual("AM23451", candidate.CandidatePhotoIdentification.Number);
            Assert.AreEqual(2, candidate.CandidatePhotoIdentification.PhotoIdentificationId);
            Assert.AreEqual(new DateTime(2016, 10, 4), candidate.CandidatePhotoIdentification.IssueDate);
        }

        /// <summary>
        /// Verifies that a candidate can be updated in the database
        /// </summary>
        [TestMethod]
        public async Task TestUpdateCandidate()
        {
            // Arrange
            var candidateId = 1;

            var updatedCandidate = new Candidate
            {
                FirstName = "Vlasis T",
                MiddleName = "T",
                LastName = "Mavraganis T",
                Gender = GendersEnum.Female.ToString(),
                NativeLanguage = LanguagesEnum.English.ToString(),
                BirthDate = new DateTime(2021, 12, 7),
                CandidateLocation = new CandidateLocation
                {
                    Address = "Laurem Ipsum T",
                    Address2 = "T",
                    City = "Consectetur T",
                    Province = "Gorlem T",
                    Residence = "Dolor Sit T",
                    PostalCode = "11111"
                },
                CandidateContact = new CandidateContact
                {
                    MobileNumber = "1111111111",
                    LandlineNumber = "2222222222",
                    Email = "test@vlasis.gr"
                },
                CandidatePhotoIdentification = new CandidatePhotoIdentification
                {
                    Number = "TT12312",
                    IssueDate = new DateTime(2016, 2, 2),
                    PhotoIdentificationId = 2,
                }
            };

            // Act
            var candidateToUpdate = await _adminrepository.GetCandidate(candidateId);
            candidateToUpdate.FirstName = updatedCandidate.FirstName;
            candidateToUpdate.MiddleName = updatedCandidate.MiddleName;
            candidateToUpdate.LastName = updatedCandidate.LastName;
            candidateToUpdate.Gender = updatedCandidate.Gender;
            candidateToUpdate.NativeLanguage = updatedCandidate.NativeLanguage;
            candidateToUpdate.BirthDate = updatedCandidate.BirthDate;
            candidateToUpdate.CandidateLocation.Address = updatedCandidate.CandidateLocation.Address;
            candidateToUpdate.CandidateLocation.Address2 = updatedCandidate.CandidateLocation.Address2;
            candidateToUpdate.CandidateLocation.City = updatedCandidate.CandidateLocation.City;
            candidateToUpdate.CandidateLocation.Province = updatedCandidate.CandidateLocation.Province;
            candidateToUpdate.CandidateLocation.Residence = updatedCandidate.CandidateLocation.Residence;
            candidateToUpdate.CandidateLocation.PostalCode = updatedCandidate.CandidateLocation.PostalCode;
            candidateToUpdate.CandidateContact.MobileNumber = updatedCandidate.CandidateContact.MobileNumber;
            candidateToUpdate.CandidateContact.LandlineNumber = updatedCandidate.CandidateContact.LandlineNumber;
            candidateToUpdate.CandidateContact.Email = updatedCandidate.CandidateContact.Email;
            candidateToUpdate.CandidatePhotoIdentification.Number = updatedCandidate.CandidatePhotoIdentification.Number;
            candidateToUpdate.CandidatePhotoIdentification.IssueDate = updatedCandidate.CandidatePhotoIdentification.IssueDate;
            candidateToUpdate.CandidatePhotoIdentification.PhotoIdentificationId = updatedCandidate.CandidatePhotoIdentification.PhotoIdentificationId;

            await _adminrepository.UpdateCandidate(candidateToUpdate);

            // Assert
            var updatedCandidateOnDB = await _adminrepository.GetCandidate(candidateId);
            Assert.AreEqual(updatedCandidate.FirstName, updatedCandidateOnDB.FirstName);
            Assert.AreEqual(updatedCandidate.MiddleName, updatedCandidateOnDB.MiddleName);
            Assert.AreEqual(updatedCandidate.LastName, updatedCandidateOnDB.LastName);
            Assert.AreEqual(updatedCandidate.Gender, updatedCandidateOnDB.Gender);
            Assert.AreEqual(updatedCandidate.NativeLanguage, updatedCandidateOnDB.NativeLanguage);
            Assert.AreEqual(updatedCandidate.BirthDate, updatedCandidateOnDB.BirthDate);
            Assert.AreEqual(updatedCandidate.CandidateLocation.Address, updatedCandidateOnDB.CandidateLocation.Address);
            Assert.AreEqual(updatedCandidate.CandidateLocation.Address2, updatedCandidateOnDB.CandidateLocation.Address2);
            Assert.AreEqual(updatedCandidate.CandidateLocation.City, updatedCandidateOnDB.CandidateLocation.City);
            Assert.AreEqual(updatedCandidate.CandidateLocation.Province, updatedCandidateOnDB.CandidateLocation.Province);
            Assert.AreEqual(updatedCandidate.CandidateLocation.Residence, updatedCandidateOnDB.CandidateLocation.Residence);
            Assert.AreEqual(updatedCandidate.CandidateLocation.PostalCode, updatedCandidateOnDB.CandidateLocation.PostalCode);
            Assert.AreEqual(updatedCandidate.CandidateContact.MobileNumber, updatedCandidateOnDB.CandidateContact.MobileNumber);
            Assert.AreEqual(updatedCandidate.CandidateContact.LandlineNumber, updatedCandidateOnDB.CandidateContact.LandlineNumber);
            Assert.AreEqual(updatedCandidate.CandidateContact.Email, updatedCandidateOnDB.CandidateContact.Email);
            Assert.AreEqual(updatedCandidate.CandidatePhotoIdentification.Number, updatedCandidateOnDB.CandidatePhotoIdentification.Number);
            Assert.AreEqual(updatedCandidate.CandidatePhotoIdentification.PhotoIdentificationId, updatedCandidateOnDB.CandidatePhotoIdentification.PhotoIdentificationId);
        }

        /// <summary>
        /// Verifies that a candidate can be deleted from the database
        /// </summary>
        [TestMethod]
        public async Task TestDeleteCandidate()
        {
            // Arrange
            var candidateId = 3;

            // Act
            await _adminrepository.DeleteCandidate(candidateId);

            // Assert
            var deletedCandidate = await _adminrepository.GetCandidate(candidateId);
            Assert.IsNull(deletedCandidate);
        }
    }
}
