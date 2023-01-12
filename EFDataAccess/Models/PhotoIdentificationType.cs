using System.ComponentModel.DataAnnotations;

namespace EFDataAccess.Models
{
    public class PhotoIdentificationType
    {
        public int PhotoIdentificationTypeId { get; set; }
        [MaxLength(80)]
        public string Type { get; set; }
    }
}
