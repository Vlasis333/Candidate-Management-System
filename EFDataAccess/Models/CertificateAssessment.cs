using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccess.Models
{
    public class CertificateAssessment
    {
        [ForeignKey("CandidateCertificates")]
        public int CertificateAssessmentId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ScoreReportDate { get; set; }
        public Int16 CandidateScore { get; set; }
        public Int16 MaximumScore { get; set; }
        public Int16 PercentageScore { get; set; }
        [MaxLength(40)]
        public string AssessmentResult { get; set; }

        public virtual CandidateCertificates CandidateCertificates { get; set; }
    }
}
