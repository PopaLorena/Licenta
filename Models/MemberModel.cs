using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models
{
    public class MemberModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer ten 50 char")]
        public string  Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Email can't be longer ten 50 char")]
        public string Email { get; set; }

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

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Responsibility> Responsibilities { get; set; } = new List<Responsibility>();

        public IList<MemberMeeting> MemberMeetings { get; set; } = new List<MemberMeeting>();

        public IList<MemberTraining> MemberTrainings { get; set; } = new List<MemberTraining>();
    }
}
