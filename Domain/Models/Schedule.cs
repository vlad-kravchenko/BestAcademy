using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Group Group { get; set; }
        [Required]
        public Lesson Lesson { get; set; }
        [Required]
        public Subject Subject { get; set; }
    }
}