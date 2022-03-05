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

        Task<Meeting> GetMeetingById(int id);

        Task<Meeting> AddMeeting(Meeting meeting);

        Task DeleteMeeting(Meeting meeting);

        Task<Meeting> EditMeeting(Meeting meeting);
    }
}
