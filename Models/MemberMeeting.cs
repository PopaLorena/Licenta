using System;

namespace Licenta.Models
{
    public class MemberMeeting
    {
        public int MemberId { get; set; }
        public MemberModel Member { get; set; }

        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
