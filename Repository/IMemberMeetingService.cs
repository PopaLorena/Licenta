using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface IMemberMeetingService
    {
        Task<List<Meeting>> GetMeetingsByMemberId(int memberId);
        Task<List<MemberModel>> GetMembersByMeetingsId(int meetingId);

        Task<MemberMeeting> AddMemberToMeeting(int memberId, int meetingId);

        Task DeleteMemberFromMeeting(MemberMeeting memberMeeting);

    }
}
