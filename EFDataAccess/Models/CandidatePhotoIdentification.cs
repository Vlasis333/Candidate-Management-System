using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccess.Models
{
    public class CandidatePhotoIdentification
    {
        [ForeignKey("Candidate")]
        public int CandidatePhotoIdentificationId { get; set; }
        [ForeignKey("PhotoIdentificationType")]
        public int PhotoIdentificationId { get; set; }
        [MaxLength(40)]
        [StringLength(40, MinimumLength = 0, ErrorMessage = "Characters from 0 to 40")]
        public string Number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual PhotoIdentificationType PhotoIdentificationType { get; set; }
    }
}
