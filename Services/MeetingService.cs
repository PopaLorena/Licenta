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

        public Meeting AddMeeting(Meeting meeting)
        {
            meeting.Id = Guid.NewGuid();
            _context.Meetings.Add(meeting);
            _context.SaveChanges();
            return meeting;
        }

        public void DeleteMeeting(Meeting meeting)
        {
            _context.Meetings.Remove(meeting);
            _context.SaveChanges();
        }

        public Meeting EditMeeting(Meeting meeting)
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

        public Meeting GetMeetingById(Guid id)
        {
            return _context.Meetings.SingleOrDefault(x => x.Id == id);
        }

        public List<Meeting> GetMeetings()
        {
            return _context.Meetings.ToList();
        }

        public Meeting AddParticipants(Guid id, MemberModel member)
        {
            var existingMeeting = _context.Meetings.SingleOrDefault(x => x.Id == id);
            existingMeeting.Participants.Add(member);
            _context.Meetings.Update(existingMeeting);
            AddMeetingToMember(existingMeeting, member.Id);
            _context.SaveChanges();

            return existingMeeting;
        }

        private void AddMeetingToMember(Meeting meeting, Guid memberId)
        {
            MemberModel member = memberService.GetMemberById(memberId);

            member.Meetings.Add(meeting);
            memberService.EditMember(member);
        }
    }
}
