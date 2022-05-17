using Licenta.Models;
using Licenta.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class MeetingService : IMeetingService
    {
        private Context.ContextDb _context;

        public MeetingService(Context.ContextDb context, IMemberService memberService)
        {
            _context = context;
        }

        public async Task<Meeting> AddMeeting(Meeting meeting)
        {
            _context.Meetings.Add(meeting);
            _context.SaveChanges();
            return meeting;
        }

        public async Task DeleteMeeting(Meeting meeting)
        {
            _context.Meetings.Remove(meeting);
            _context.SaveChanges();
        }

        public async Task<Meeting> EditMeeting(Meeting meeting)
        {
            var existingMeeting = _context.Meetings.Find(meeting.Id);
            if (existingMeeting != null)
            {
                existingMeeting.Name = meeting.Name;
                existingMeeting.Date = meeting.Date;
                existingMeeting.FacilitatorName = meeting.FacilitatorName;
                existingMeeting.Participants = meeting.Participants;

                _context.Meetings.Update(existingMeeting);
                _context.SaveChanges();
            }
            return meeting;
        }

        public async Task<Meeting> GetMeetingById(int id)
        {
            return _context.Meetings.SingleOrDefault(x => x.Id == id);
        }

        public async Task<List<Meeting>> GetMeetingByMemberId(int id)
        {
            var meetings = from r in _context.MemberMeetings
                           where (r.MemberId == id)
                           select r.Meeting;
            return meetings.ToList();
        }

        public async Task<List<MemberModel>> GetParicipants(int id)
        {
            var members = from r in _context.MemberMeetings
                           where (r.MeetingId == id)
                           select r.Member;
            return members.ToList();
        }

        public async Task<List<Meeting>> GetMeetings()
        {
            return _context.Meetings.Include(m => m.Participants).ToList();
        }

        public async Task<List<Meeting>> GetSortMeetings()
        {
            var date = DateTime.Now;
            var ActiveMeeting = _context.Meetings.Where(m => m.Date >= date);

            return ActiveMeeting.OrderBy(m => m.Date).Include(m => m.Participants).ToList();
        }

        public async Task<Meeting> GetNextMeeting()
        {
            var date = DateTime.Now;
            var ActiveMeeting = _context.Meetings.Where(m => m.Date >= date);

            return ActiveMeeting.OrderBy(m => m.Date).Include(m => m.Participants).ToList()[0];
        }
    }
}

