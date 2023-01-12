using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccess.Models
{
    public class CandidateContact
    {
        [ForeignKey("Candidate")]
        public int CandidateContactId { get; set; }
        [MaxLength(120)]
        [StringLength(120, MinimumLength = 0, ErrorMessage = "Characters from 0 to 120")]
        [DisplayName("Landline Number")]
        public string LandlineNumber { get; set; }
        [MaxLength(120)]
        [StringLength(120, MinimumLength = 0, ErrorMessage = "Characters from 0 to 120")]
        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}
