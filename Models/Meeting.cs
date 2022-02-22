using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models
{
    public class Meeting
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer ten 50 char")]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string FacilitatorName { get; set; }

        public List<MemberModel> Participants { get; set; }
    }
}
