using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface IMeetingService
    {
        Task<List<Meeting>> GetMeetings();

        Task<List<Meeting>> GetSortMeetings();
        Task<Meeting> GetNextMeeting();

        Task<List<Meeting>> GetMeetingByMemberId(int id);

        Task<List<MemberModel>> GetParicipants(int id);

        Task<Meeting> GetMeetingById(int id);

        Task<Meeting> AddMeeting(Meeting meeting);

        Task DeleteMeeting(Meeting meeting);

        Task<Meeting> EditMeeting(Meeting meeting);
    }
}
