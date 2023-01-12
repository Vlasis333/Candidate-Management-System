using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFDataAccess.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        [Required]
        [MaxLength(100)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Characters from 1 to 100")]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Characters from 0 to 100")]
        public string MiddleName { get; set; }
        [MaxLength(100)]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Characters from 0 to 100")]
        public string LastName { get; set; }
        [MaxLength(60)]
        [StringLength(60, MinimumLength = 0, ErrorMessage = "Characters from 0 to 60")]
        public string Gender { get; set; }
        [MaxLength(100)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Characters from 1 to 100")]
        public string NativeLanguage { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        // Connections to other entities
        public virtual CandidateLocation CandidateLocation { get; set; }
        public virtual CandidateContact CandidateContact { get; set; }
        public virtual CandidatePhotoIdentification CandidatePhotoIdentification { get; set; }
        // Connection to the Certificates entity
        public ICollection<CandidateCertificates> CandidateCertificates { get; set; }
    }
}
