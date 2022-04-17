using Licenta.Models;
using Licenta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class MemberMeetingService : IMemberMeetingService
    {
        private readonly Context.ContextDb _context;
        private readonly IMeetingService meetingService;
        private readonly IMemberService memberService;
        public MemberMeetingService(Context.ContextDb context, IMeetingService meetingService, IMemberService memberService)
        {
            _context = context;
            this.meetingService = meetingService;
            this.memberService = memberService;
        }

        public async Task<MemberMeeting> AddMemberToMeeting(int memberId, int meetingId)
        {
            MemberMeeting memberMeeting = new MemberMeeting();
            var member = await memberService.GetMemberById(memberId).ConfigureAwait(false);
            var meeting = await meetingService.GetMeetingById(meetingId).ConfigureAwait(false);

            memberMeeting.MeetingId = meetingId;
            memberMeeting.Member = member;
            memberMeeting.MemberId = memberId;
            memberMeeting.Meeting = meeting;

            _context.MemberMeetings.Add(memberMeeting);
            _context.SaveChanges();

            return memberMeeting;
        }

        public async Task<bool> CheckIfExist(int memberId, int meetingId)
        {
            IEnumerable<MemberMeeting> memberMeetings = from m in _context.MemberMeetings
                           where (m.MemberId == memberId && m.MeetingId == meetingId) 
                           select m;

            if (!memberMeetings.Any())
                return false;
            return true;
        }

        public async Task DeleteMemberFromMeeting(int memberId, int meetingId)
        {
            MemberMeeting memberMeeting = new MemberMeeting();

            memberMeeting = _context.MemberMeetings.Single(c => c.MeetingId == meetingId && c.MemberId == memberId);
           
            var member = await memberService.GetMemberById(memberMeeting.MemberId).ConfigureAwait(false);
            var meeting = await meetingService.GetMeetingById(memberMeeting.MeetingId).ConfigureAwait(false);
          
            if (memberMeeting.Member == null)
            {
                memberMeeting.Member = member;
            }
            if (memberMeeting.Meeting == null)
            {
                memberMeeting.Meeting = meeting;
            }

            _context.MemberMeetings.Remove(memberMeeting);
            _context.SaveChanges();
        }

        public async Task<List<Meeting>> GetMeetingsByMemberId(int memberId)
        {
            var meetings = from m in _context.MemberMeetings
                           where (m.MemberId == memberId)
                           select m.Meeting;

            return meetings.ToList();
        }

        public async Task<List<MemberModel>> GetMembersByMeetingsId(int meetingId)
        {
            var members = from m in _context.MemberMeetings
                          where (m.MeetingId == meetingId)
                          select m.Member;

            return members.ToList();
        }
    }
}
