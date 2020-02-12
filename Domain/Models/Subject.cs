using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Subject : IComparable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        public int CompareTo(object obj)
        {
            return Name.CompareTo((obj as Subject).Name);
        }
        public string FullName { get { return $"{Name} ({Teacher.Name})"; } }
    }
}