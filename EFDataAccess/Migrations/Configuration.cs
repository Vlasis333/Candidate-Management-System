namespace EFDataAccess.Migrations
{
    using EFDataAccess.Helpers;
    using EFDataAccess.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFDataAccess.Data.AppDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EFDataAccess.Data.AppDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // Initial Dummy Data
            try
            {
                // we Load the data in this order so all will be populated correctly 
                AddInitialTopics(context); // relationship needed
                AddInitialTopicsAssessments(context);

                AddInitialCertificates(context); // relationship needed
                AddInitialCandidateCertificates(context);
                AddInitialCertificateAssessments(context);

                AddInitialCandidates(context); // relationship needed
                AddInitialCandidatesContacts(context);
                AddInitialCandidatesLocations(context);

                AddInitialPhotoIdentificationTypes(context); // relationship needed
                AddInitialCandidatesPhotoIdentifications(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region Load Dummy Data
        public void AddInitialPhotoIdentificationTypes(EFDataAccess.Data.AppDBContext context)
        {
            if (context.PhotoIdentificationTypes.Where(c => c.PhotoIdentificationTypeId == 1).Count() == 0 &&
                context.PhotoIdentificationTypes.Where(c => c.PhotoIdentificationTypeId == 2).Count() == 0 &&
                context.PhotoIdentificationTypes.Where(c => c.PhotoIdentificationTypeId == 3).Count() == 0 &&
                context.PhotoIdentificationTypes.Where(c => c.PhotoIdentificationTypeId == 4).Count() == 0 &&
                context.PhotoIdentificationTypes.Where(c => c.PhotoIdentificationTypeId == 5).Count() == 0)
            {
                context.PhotoIdentificationTypes.Add(new PhotoIdentificationType
                { Type = "Passport" });
                context.PhotoIdentificationTypes.Add(new PhotoIdentificationType
                { Type = "Driver Licence" });
                context.PhotoIdentificationTypes.Add(new PhotoIdentificationType
                { Type = "Identity Card" });
                context.PhotoIdentificationTypes.Add(new PhotoIdentificationType
                { Type = "Company Id Card" });
                context.PhotoIdentificationTypes.Add(new PhotoIdentificationType
                { Type = "Military Id" });
                context.SaveChanges();
            }
        }

        public void AddInitialTopics(EFDataAccess.Data.AppDBContext context)
        {
            if (context.Topics.Where(c => c.TopicId == 1).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 2).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 3).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 4).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 5).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 6).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 7).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 8).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 9).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 10).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 11).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 12).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 13).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 14).Count() == 0 &&
                context.Topics.Where(c => c.TopicId == 15).Count() == 0)
            {
                context.Topics.Add(new Topic
                { Category = 100, Title = "Developer Foundation Level", Description = "Fullstack Foundation Developer Coding Bootcamp", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 101, Title = "Developer Advanced Level", Description = "Fullstack Advanced Developer Coding Bootcamp", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 200, Title = "English Foundation Level", Description = "Proficiency on English Language Foundation", PossibleMarks = 25 });
                context.Topics.Add(new Topic
                { Category = 201, Title = "English Advanced Level", Description = "Proficiency on English Language Advanced", PossibleMarks = 25 });
                context.Topics.Add(new Topic
                { Category = 202, Title = "English Expert Level", Description = "Proficiency on English Language Expert", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 300, Title = "MBA Foundation", Description = "MBA Logistics And Python Foundation", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 301, Title = "MBA Advanced", Description = "MBA Logistics And Python Advanced", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 400, Title = "Nursing Essentials", Description = "'Nursing Studies Basics", PossibleMarks = 25 });
                context.Topics.Add(new Topic
                { Category = 401, Title = "First Aid", Description = "Nursing Studies FA", PossibleMarks = 25 });
                context.Topics.Add(new Topic
                { Category = 402, Title = "Medicine Ethics", Description = "Nursing Studies ME", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 500, Title = "Nursing Essentials 2", Description = "'Nursing Studies Basics 2", PossibleMarks = 25 });
                context.Topics.Add(new Topic
                { Category = 501, Title = "First Aid 2", Description = "Nursing Studies FA 2", PossibleMarks = 25 });
                context.Topics.Add(new Topic
                { Category = 502, Title = "Medicine Ethics 2", Description = "Nursing Studies ME 2", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 600, Title = "Driving Skills Entry", Description = "Driving Skills Ent", PossibleMarks = 50 });
                context.Topics.Add(new Topic
                { Category = 601, Title = "Driving Skills Advanced", Description = "Driving Skills Adv", PossibleMarks = 50 });
                context.SaveChanges();
            }
        }

        private void AddInitialCandidates(EFDataAccess.Data.AppDBContext context)
        {
            if (context.Candidates.Where(c => c.CandidateId == 1).Count() == 0 &&
                context.Candidates.Where(c => c.CandidateId == 2).Count() == 0 &&
                context.Candidates.Where(c => c.CandidateId == 3).Count() == 0 &&
                context.Candidates.Where(c => c.CandidateId == 4).Count() == 0)
            {
                context.Candidates.Add(new Candidate
                {
                    FirstName = "Vlasis",
                    MiddleName = "",
                    LastName = "Mavraganis",
                    Gender = GendersEnum.Male.ToString(),
                    NativeLanguage = LanguagesEnum.Greek.ToString(),
                    BirthDate = new DateTime(1994, 1, 6),
                    CandidateCertificates = context.CandidateCertificates.Where(p => p.CandidateCertificatesId == 1).ToList()
                });
                context.Candidates.Add(new Candidate
                {
                    FirstName = "Ina",
                    MiddleName = "",
                    LastName = "Bogdani",
                    Gender = GendersEnum.Female.ToString(),
                    NativeLanguage = LanguagesEnum.Albanian.ToString(),
                    BirthDate = new DateTime(1997, 6, 4),
                    CandidateCertificates = context.CandidateCertificates.Where(p => p.CandidateCertificatesId == 2 || p.CandidateCertificatesId == 3).ToList()
                });
                context.Candidates.Add(new Candidate
                {
                    FirstName = "Laurem",
                    MiddleName = "Puren",
                    LastName = "Ipsum",
                    Gender = GendersEnum.Binary.ToString(),
                    NativeLanguage = LanguagesEnum.English.ToString(),
                    BirthDate = new DateTime(1999, 07, 12),
                    CandidateCertificates = context.CandidateCertificates.Where(p => p.CandidateCertificatesId == 4).ToList()
                });
                context.Candidates.Add(new Candidate
                {
                    FirstName = "Efi",
                    MiddleName = "",
                    LastName = "Mavragani",
                    Gender = GendersEnum.Female.ToString(),
                    NativeLanguage = LanguagesEnum.Greek.ToString(),
                    BirthDate = new DateTime(1997, 6, 4),
                    CandidateCertificates = context.CandidateCertificates.Where(p => p.CandidateCertificatesId == 5 || p.CandidateCertificatesId == 6).ToList()
                });
                context.SaveChanges();
            }
        }

        private void AddInitialCandidatesContacts(EFDataAccess.Data.AppDBContext context)
        {
            if (context.CandidateContacts.Where(c => c.CandidateContactId == 1).Count() == 0 &&
                context.CandidateContacts.Where(c => c.CandidateContactId == 2).Count() == 0 &&
                context.CandidateContacts.Where(c => c.CandidateContactId == 3).Count() == 0 &&
                context.CandidateContacts.Where(c => c.CandidateContactId == 4).Count() == 0)
            {
                context.CandidateContacts.Add(new CandidateContact
                { CandidateContactId = 1, LandlineNumber = "210-1234567", MobileNumber = "6971234567", Email = "vlasis@vlasis.gr" });
                context.CandidateContacts.Add(new CandidateContact
                { CandidateContactId = 2, LandlineNumber = "210-1234567", MobileNumber = "1947274863", Email = "ina@gmail.com" });
                context.CandidateContacts.Add(new CandidateContact
                { CandidateContactId = 3, LandlineNumber = "123123456", MobileNumber = "00000000000", Email = "test@testo.ts" });
                context.CandidateContacts.Add(new CandidateContact
                { CandidateContactId = 4, LandlineNumber = "210-9999999", MobileNumber = "1321321321", Email = "efi@hotmail.gr" });
                context.SaveChanges();
            }
        }

        private void AddInitialCandidatesLocations(EFDataAccess.Data.AppDBContext context)
        {
            if (context.CandidateLocations.Where(c => c.CandidateLocationId == 1).Count() == 0 &&
                context.CandidateLocations.Where(c => c.CandidateLocationId == 2).Count() == 0 &&
                context.CandidateLocations.Where(c => c.CandidateLocationId == 3).Count() == 0 &&
                context.CandidateLocations.Where(c => c.CandidateLocationId == 4).Count() == 0)
            {
                context.CandidateLocations.Add(new CandidateLocation
                {
                    CandidateLocationId = 1,
                    Address = "Laurem Ipsum",
                    Residence = "Dolor Sit",
                    Province = "Gorlem",
                    City = "Consectetur",
                    PostalCode = "12345"
                });
                context.CandidateLocations.Add(new CandidateLocation
                {
                    CandidateLocationId = 2,
                    Address = "Eget Felis",
                    Residence = "Suspendisse",
                    Province = "Nam",
                    City = "Fusce",
                    PostalCode = "12345"
                });
                context.CandidateLocations.Add(new CandidateLocation
                {
                    CandidateLocationId = 3,
                    Address = "TestArea",
                    Address2 = "TestArea 2",
                    Residence = "Testiki",
                    Province = "TestPro",
                    City = "TestCity",
                    PostalCode = "00000"
                });
                context.CandidateLocations.Add(new CandidateLocation
                {
                    CandidateLocationId = 4,
                    Address = "Vestibulum",
                    Residence = "Mauris",
                    City = "Aliquam",
                    PostalCode = "67890"
                });
                context.SaveChanges();
            }
        }

        private void AddInitialCandidatesPhotoIdentifications(EFDataAccess.Data.AppDBContext context)
        {
            if (context.CandidatePhotoIdentifications.Where(c => c.CandidatePhotoIdentificationId == 1).Count() == 0 &&
                context.CandidatePhotoIdentifications.Where(c => c.CandidatePhotoIdentificationId == 2).Count() == 0 &&
                context.CandidatePhotoIdentifications.Where(c => c.CandidatePhotoIdentificationId == 3).Count() == 0 &&
                context.CandidatePhotoIdentifications.Where(c => c.CandidatePhotoIdentificationId == 4).Count() == 0)
            {
                context.CandidatePhotoIdentifications.Add(new CandidatePhotoIdentification
                {
                    CandidatePhotoIdentificationId = 1,
                    Number = "AB12312",
                    IssueDate = new DateTime(2002, 11, 16),
                    PhotoIdentificationType = context.PhotoIdentificationTypes.Where(p => p.PhotoIdentificationTypeId == 1).SingleOrDefault()
                });
                context.CandidatePhotoIdentifications.Add(new CandidatePhotoIdentification
                {
                    CandidatePhotoIdentificationId = 2,
                    Number = "AM23451",
                    IssueDate = new DateTime(2016, 10, 4),
                    PhotoIdentificationType = context.PhotoIdentificationTypes.Where(p => p.PhotoIdentificationTypeId == 2).SingleOrDefault()
                });
                context.CandidatePhotoIdentifications.Add(new CandidatePhotoIdentification
                {
                    CandidatePhotoIdentificationId = 3,
                    Number = "TEST27492",
                    IssueDate = new DateTime(2022, 12, 3),
                    PhotoIdentificationType = context.PhotoIdentificationTypes.Where(p => p.PhotoIdentificationTypeId == 3).SingleOrDefault()
                });
                context.CandidatePhotoIdentifications.Add(new CandidatePhotoIdentification
                {
                    CandidatePhotoIdentificationId = 4,
                    Number = "AL28462",
                    IssueDate = new DateTime(2008, 5, 23),
                    PhotoIdentificationType = context.PhotoIdentificationTypes.Where(p => p.PhotoIdentificationTypeId == 4).SingleOrDefault()
                });
                context.SaveChanges();
            }
        }

        private void AddInitialCertificates(EFDataAccess.Data.AppDBContext context)
        {
            if (context.Certificates.Where(c => c.CertificateId == 1).Count() == 0 &&
                context.Certificates.Where(c => c.CertificateId == 2).Count() == 0 &&
                context.Certificates.Where(c => c.CertificateId == 3).Count() == 0 &&
                context.Certificates.Where(c => c.CertificateId == 4).Count() == 0 &&
                context.Certificates.Where(c => c.CertificateId == 5).Count() == 0 &&
                context.Certificates.Where(c => c.CertificateId == 6).Count() == 0)
            {
                context.Certificates.Add(new Certificate
                {
                    Title = "Coding Bootcamp",
                    AssessmentTestCode = "CB",
                    Active = true,
                    Topics = context.Topics.Where(p => p.TopicId == 1 || p.TopicId == 2).ToList()
                });
                context.Certificates.Add(new Certificate
                {
                    Title = "English",
                    AssessmentTestCode = "EN",
                    Active = true,
                    Topics = context.Topics.Where(p => p.TopicId == 3 || p.TopicId == 4 || p.TopicId == 5).ToList()
                });
                context.Certificates.Add(new Certificate
                {
                    Title = "MBA",
                    AssessmentTestCode = "MBA",
                    Active = false,
                    Topics = context.Topics.Where(p => p.TopicId == 6 || p.TopicId == 7).ToList()
                });
                context.Certificates.Add(new Certificate
                {
                    Title = "Nursing Part 1",
                    AssessmentTestCode = "NP1",
                    Active = true,
                    Topics = context.Topics.Where(p => p.TopicId == 8 || p.TopicId == 9 || p.TopicId == 10).ToList()
                });
                context.Certificates.Add(new Certificate
                {
                    Title = "Nursing Part 2",
                    AssessmentTestCode = "NP2",
                    Active = true,
                    Topics = context.Topics.Where(p => p.TopicId == 11 || p.TopicId == 12 || p.TopicId == 13).ToList()
                });
                context.Certificates.Add(new Certificate
                {
                    Title = "Driving",
                    AssessmentTestCode = "DR",
                    Active = false,
                    Topics = context.Topics.Where(p => p.TopicId == 14 || p.TopicId == 15).ToList()
                });
                context.SaveChanges();
            }
        }

        private void AddInitialCandidateCertificates(EFDataAccess.Data.AppDBContext context)
        {
            if (context.CandidateCertificates.Where(c => c.CandidateCertificatesId == 1).Count() == 0 &&
                context.CandidateCertificates.Where(c => c.CandidateCertificatesId == 2).Count() == 0 &&
                context.CandidateCertificates.Where(c => c.CandidateCertificatesId == 3).Count() == 0 &&
                context.CandidateCertificates.Where(c => c.CandidateCertificatesId == 4).Count() == 0 &&
                context.CandidateCertificates.Where(c => c.CandidateCertificatesId == 5).Count() == 0 &&
                context.CandidateCertificates.Where(c => c.CandidateCertificatesId == 6).Count() == 0)
            {
                context.CandidateCertificates.Add(new CandidateCertificates
                {
                    CandidateCertificatesId = 1,
                    ExaminationDate = new DateTime(2023, 2, 7),
                    Certificate = context.Certificates.Where(p => p.CertificateId == 1).SingleOrDefault(),
                    TopicAssesments = context.TopicAssesments.Where(p => p.TopicAssesmentId == 1 || p.TopicAssesmentId == 2).ToList()
                });
                context.CandidateCertificates.Add(new CandidateCertificates
                {
                    CandidateCertificatesId = 2,
                    ExaminationDate = new DateTime(2018, 12, 17),
                    Certificate = context.Certificates.Where(p => p.CertificateId == 4).SingleOrDefault(),
                    TopicAssesments = context.TopicAssesments.Where(p => p.TopicAssesmentId == 3 || p.TopicAssesmentId == 4 || p.TopicAssesmentId == 5).ToList()
                });
                context.CandidateCertificates.Add(new CandidateCertificates
                {
                    CandidateCertificatesId = 3,
                    ExaminationDate = new DateTime(2019, 12, 17),
                    Certificate = context.Certificates.Where(p => p.CertificateId == 5).SingleOrDefault(),
                    TopicAssesments = context.TopicAssesments.Where(p => p.TopicAssesmentId == 6 || p.TopicAssesmentId == 7 || p.TopicAssesmentId == 8).ToList()
                });
                context.CandidateCertificates.Add(new CandidateCertificates
                {
                    CandidateCertificatesId = 4,
                    ExaminationDate = new DateTime(2012, 5, 9),
                    Certificate = context.Certificates.Where(p => p.CertificateId == 6).SingleOrDefault(),
                    TopicAssesments = context.TopicAssesments.Where(p => p.TopicAssesmentId == 9 || p.TopicAssesmentId == 10).ToList()
                });
                context.CandidateCertificates.Add(new CandidateCertificates
                {
                    CandidateCertificatesId = 5,
                    ExaminationDate = new DateTime(2021, 7, 7),
                    Certificate = context.Certificates.Where(p => p.CertificateId == 3).SingleOrDefault(),
                    TopicAssesments = context.TopicAssesments.Where(p => p.TopicAssesmentId == 11 || p.TopicAssesmentId == 12).ToList()
                });
                context.CandidateCertificates.Add(new CandidateCertificates
                {
                    CandidateCertificatesId = 6,
                    ExaminationDate = new DateTime(2020, 7, 7),
                    Certificate = context.Certificates.Where(p => p.CertificateId == 6).SingleOrDefault(),
                    TopicAssesments = context.TopicAssesments.Where(p => p.TopicAssesmentId == 13 || p.TopicAssesmentId == 14).ToList()
                });
                context.SaveChanges();
            }
        }

        private void AddInitialTopicsAssessments(EFDataAccess.Data.AppDBContext context)
        {
            if (context.TopicAssesments.Where(c => c.TopicAssesmentId == 1).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 2).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 3).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 4).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 5).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 6).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 7).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 8).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 9).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 10).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 11).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 12).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 13).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 14).Count() == 0 &&
                context.TopicAssesments.Where(c => c.TopicAssesmentId == 15).Count() == 0)
            {
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 48, Topic = context.Topics.Where(p => p.TopicId == 1).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 42, Topic = context.Topics.Where(p => p.TopicId == 2).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 23, Topic = context.Topics.Where(p => p.TopicId == 8).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 25, Topic = context.Topics.Where(p => p.TopicId == 9).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 47, Topic = context.Topics.Where(p => p.TopicId == 10).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 25, Topic = context.Topics.Where(p => p.TopicId == 11).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 24, Topic = context.Topics.Where(p => p.TopicId == 12).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 49, Topic = context.Topics.Where(p => p.TopicId == 13).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 12, Topic = context.Topics.Where(p => p.TopicId == 14).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 5, Topic = context.Topics.Where(p => p.TopicId == 15).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 12, Topic = context.Topics.Where(p => p.TopicId == 6).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 9, Topic = context.Topics.Where(p => p.TopicId == 7).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 45, Topic = context.Topics.Where(p => p.TopicId == 14).SingleOrDefault() });
                context.TopicAssesments.Add(new TopicAssesment
                { AwardedMarks = 42, Topic = context.Topics.Where(p => p.TopicId == 15).SingleOrDefault() });
                context.SaveChanges();
            }
        }

        private void AddInitialCertificateAssessments(EFDataAccess.Data.AppDBContext context)
        {
            if (context.CertificateAssessments.Where(c => c.CertificateAssessmentId == 1).Count() == 0 &&
                context.CertificateAssessments.Where(c => c.CertificateAssessmentId == 2).Count() == 0 &&
                context.CertificateAssessments.Where(c => c.CertificateAssessmentId == 3).Count() == 0 &&
                context.CertificateAssessments.Where(c => c.CertificateAssessmentId == 4).Count() == 0 &&
                context.CertificateAssessments.Where(c => c.CertificateAssessmentId == 5).Count() == 0 &&
                context.CertificateAssessments.Where(c => c.CertificateAssessmentId == 6).Count() == 0)
            {
                context.CertificateAssessments.Add(new CertificateAssessment
                {
                    CertificateAssessmentId = 1,
                    ScoreReportDate = new DateTime(2023, 2, 7),
                    CandidateScore = 92,
                    MaximumScore = 100,
                    PercentageScore = 92,
                    AssessmentResult = AssessmentResultsEnum.PASSED.ToString()
                });
                context.CertificateAssessments.Add(new CertificateAssessment
                {
                    CertificateAssessmentId = 2,
                    ScoreReportDate = new DateTime(2018, 12, 17),
                    CandidateScore = 99,
                    MaximumScore = 100,
                    PercentageScore = 99,
                    AssessmentResult = AssessmentResultsEnum.PASSED.ToString()
                });
                context.CertificateAssessments.Add(new CertificateAssessment
                {
                    CertificateAssessmentId = 3,
                    ScoreReportDate = new DateTime(2019, 12, 17),
                    CandidateScore = 100,
                    MaximumScore = 100,
                    PercentageScore = 100,
                    AssessmentResult = AssessmentResultsEnum.PASSED.ToString()
                });
                context.CertificateAssessments.Add(new CertificateAssessment
                {
                    CertificateAssessmentId = 4,
                    ScoreReportDate = new DateTime(2012, 5, 9),
                    CandidateScore = 4,
                    MaximumScore = 100,
                    PercentageScore = 4,
                    AssessmentResult = AssessmentResultsEnum.FAILED.ToString()
                });
                context.CertificateAssessments.Add(new CertificateAssessment
                {
                    CertificateAssessmentId = 5,
                    ScoreReportDate = new DateTime(2023, 2, 7),
                    CandidateScore = 64,
                    MaximumScore = 100,
                    PercentageScore = 64,
                    AssessmentResult = AssessmentResultsEnum.FAILED.ToString()
                });
                context.CertificateAssessments.Add(new CertificateAssessment
                {
                    CertificateAssessmentId = 6,
                    ScoreReportDate = new DateTime(2020, 7, 7),
                    CandidateScore = 96,
                    MaximumScore = 100,
                    PercentageScore = 96,
                    AssessmentResult = AssessmentResultsEnum.PASSED.ToString()
                });
                context.SaveChanges();
            }
        }
        #endregion
    }
}
