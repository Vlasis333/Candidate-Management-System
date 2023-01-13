using System;
using System.ComponentModel;

namespace EFDataAccess.Models
{
    public class TopicAssesment
    {
        public int TopicAssesmentId { get; set; }
        [DisplayName("Awarded Marks")]
        public Int16 AwardedMarks { get; set; }

        public virtual CandidateCertificates CandidateCertificates { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
