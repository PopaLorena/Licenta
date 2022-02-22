using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models
{
    public class MemberModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer ten 50 char")]
        public string  Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Email can't be longer ten 50 char")]
        public string Email { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "PhotoUrl can't be longer ten 50 char")]
        public string PhotoUrl { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Statut can't be longer ten 50 char")]
        public string Statut { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StatutChangeDate { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "University can't be longer ten 50 char")]
        public string University { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "TelNumber can't be longer ten 50 char")]
        public string TelNumber { get; set; }

        public List<Task> Tasks { get => tasks; set => tasks = value; }

        public List<Training> Trainings { get => trainings; set => trainings = value; }

        public List<Meeting> Meetings { get => meetings; set => meetings = value; }

        private List<Task> tasks = new List<Task>();

        private List<Training> trainings = new List<Training>();

        private List<Meeting> meetings = new List<Meeting>();
    }
}
