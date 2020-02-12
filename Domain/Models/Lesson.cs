using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string Start { get; set; }

        public override string ToString()
        {
            return Day + " " + Start;
        }
    }
}