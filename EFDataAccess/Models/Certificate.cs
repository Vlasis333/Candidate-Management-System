using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFDataAccess.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        [Required]
        [MaxLength(70)]
        public string Title { get; set; }
        [MaxLength(30)]
        public string AssessmentTestCode { get; set; }
        public bool Active { get; set; }

        // Connections to other entities
        public ICollection<Topic> Topics { get; set; }
    }
}
