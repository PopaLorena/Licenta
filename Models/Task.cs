using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer ten 50 char")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Description can't be longer ten 50 char")]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public Guid ResponsibleId { get; set; }

        [Required]
        public Guid EventId { get; set; }
    }
}
