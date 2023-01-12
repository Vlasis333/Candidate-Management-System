using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccess.Models
{
    public class CandidateLocation
    {
        [ForeignKey("Candidate")]
        public int CandidateLocationId { get; set; }
        [MaxLength(120)]
        [StringLength(120, MinimumLength = 0, ErrorMessage = "Characters from 0 to 120")]
        public string Address { get; set; }
        [MaxLength(120)]
        [StringLength(120, MinimumLength = 0, ErrorMessage = "Characters from 0 to 120")]
        public string Address2 { get; set; }
        [MaxLength(120)]
        [StringLength(120, MinimumLength = 0, ErrorMessage = "Characters from 0 to 120")]
        public string Residence { get; set; }
        [MaxLength(120)]
        [StringLength(120, MinimumLength = 0, ErrorMessage = "Characters from 0 to 120")]
        public string Province { get; set; }
        [MaxLength(100)]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Characters from 0 to 100")]
        public string City { get; set; }
        [MaxLength(15)]
        [StringLength(15, MinimumLength = 0, ErrorMessage = "Characters from 0 to 15")]
        public string PostalCode { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}
