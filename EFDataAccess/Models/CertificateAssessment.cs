using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccess.Models
{
    public class CertificateAssessment
    {
        [ForeignKey("CandidateCertificates")]
        public int CertificateAssessmentId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Score Report Date")]
        public DateTime ScoreReportDate { get; set; }
        [DisplayName("Candidate Score")]
        public Int16 CandidateScore { get; set; }
        [DisplayName("Maximum Score")]
        public Int16 MaximumScore { get; set; }
        [DisplayName("Percentage Score")]
        public Int16 PercentageScore { get; set; }
        [DisplayName("Assessment Result")]
        [MaxLength(40)]
        public string AssessmentResult { get; set; }

        public virtual CandidateCertificates CandidateCertificates { get; set; }
    }
}
