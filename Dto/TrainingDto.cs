using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Dto
{
    public class TrainingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string TrainerName { get; set; }

        public IList<MemberTraining> Participants { get; set; } = new List<MemberTraining>();
    }
}
