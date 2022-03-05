using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Dto
{
    public class MeetingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string FacilitatorName { get; set; }

        public IList<MemberMeeting> Participants { get; set; } = new List<MemberMeeting>();
    }
}
