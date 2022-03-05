using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Dto
{
    public class MemberTrainingDto
    { 
        public int Id { get; set; }

        public int MemberId { get; set; }
        public MemberModel Member { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
