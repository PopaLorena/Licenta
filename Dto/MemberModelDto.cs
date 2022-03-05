using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Dto
{
    public class MemberModelDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        public string Statut { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StatutChangeDate { get; set; }

        public DateTime BirthDate { get; set; }

        public string University { get; set; }

        public string TelNumber { get; set; }

        public ICollection<Responsibility> Responsibilities { get; set; } = new List<Responsibility>();

        public IList<MemberMeeting> MemberMeetings { get; set; } = new List<MemberMeeting>();

        public IList<MemberTraining> MemberTrainings { get; set; } = new List<MemberTraining>();
    }
}
