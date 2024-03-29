﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models
{
    public class Training
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can't be longer then 50 char")]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string TrainerName { get; set; }

        public IList<MemberTraining> Participants { get; set; } = new List<MemberTraining>();
    }
}
