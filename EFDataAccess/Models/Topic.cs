using System;
using System.ComponentModel.DataAnnotations;

namespace EFDataAccess.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public int Category { get; set; }
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }
        [MaxLength(120)]
        public string Description { get; set; }
        public Int16 PossibleMarks { get; set; }
    }
}
