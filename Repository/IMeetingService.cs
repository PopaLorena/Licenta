using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface IMeetingService
    {
        List<Meeting> GetMeetings();

        Meeting GetMeetingById(Guid id);

        Meeting AddMeeting(Meeting meeting);

        void DeleteMeeting(Meeting meeting);

        Meeting EditMeeting(Meeting meeting);
    }
}
