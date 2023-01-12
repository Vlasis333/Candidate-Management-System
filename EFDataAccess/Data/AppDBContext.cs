using System.Data.Entity;
using EFDataAccess.Models;

namespace EFDataAccess.Data
{
    public class AppDBContext : DbContext
    {
        // lazy loading data
        public virtual DbSet<Candidate> Candidates { get; set; } 
        public virtual DbSet<CandidateContact> CandidateContacts { get; set; }
        public virtual DbSet<CandidateLocation> CandidateLocations { get; set; }
        public virtual DbSet<PhotoIdentificationType> PhotoIdentificationTypes { get; set; }
        public virtual DbSet<CandidatePhotoIdentification> CandidatePhotoIdentifications { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CandidateCertificates> CandidateCertificates { get; set; }
        public virtual DbSet<CertificateAssessment> CertificateAssessments { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<TopicAssesment> TopicAssesments { get; set; }

        public AppDBContext() : base("name=Assigment3BConnectionString")
        {

        }
    }
}
