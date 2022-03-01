using Licenta.Models;
using Licenta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class MeetingService : IMeetingService
    {
        private Context.ContextDb _context;
        private readonly IMemberService memberService;
        public MeetingService(Context.ContextDb context, IMemberService memberService)
        {
            _context = context;
            this.memberService = memberService;
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

        public async Task<List<Meeting>> GetMeetings()
        {
            return _context.Meetings.ToList();
        }

       
    }
}
