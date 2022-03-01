using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Licenta.Models
{
    public class Responsibility
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer ten 50 char")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Description can't be longer ten 50 char")]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public MemberModel Responsible { get; set; }

        public int ResponsibleId { get; set; }

        [JsonIgnore]
        public Event Event { get; set; }

        public int EventId { get; set; }
    }
}
