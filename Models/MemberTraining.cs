using System;

namespace Licenta.Models
{
    public class MemberTraining
    {
        public int MemberId { get; set; }
        public MemberModel Member { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
