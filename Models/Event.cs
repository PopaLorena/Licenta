using System;
using System.ComponentModel.DataAnnotations;

namespace Licenta.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer ten 50 char")]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
