using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Dto
{
    public class MemberMeetingDto
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public MemberModel Member { get; set; }

        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
