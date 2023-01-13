using EFDataAccess.Data.Repositories;
using EFDataAccess.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using EFDataAccess.Helpers;
using EFDataAccess.Models;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CertificateManagerServiceTests
    {
        private readonly ICertificateManagerRepository _certificateManagerRepository = new CertificateManagerRepository(new AppDBContext());

        [TestMethod]
        public async Task TestGetCertificate()
        {
            // Arrange
            var certificateId = 1;

            // Act
            var certificate = await _certificateManagerRepository.GetCertificate(certificateId);

            // Assert
            Assert.IsNotNull(certificate);
            Assert.AreEqual("Coding Bootcamp", certificate.Title);
            Assert.AreEqual("CB", certificate.AssessmentTestCode);
            Assert.AreEqual(true, certificate.Active);
        }
    }
}
