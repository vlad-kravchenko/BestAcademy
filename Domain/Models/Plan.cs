using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Group Group { get; set; }
        [Required]
        public Subject Subject { get; set; }
        [Required]
        public int Hours { get; set; }
    }
}