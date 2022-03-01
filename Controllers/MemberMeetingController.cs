using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/memberMeeting")]
    [ApiController]
    public class MemberMeetingController : ControllerBase
    {  
        private readonly IMemberMeetingService memberMeetingService;
        public MemberMeetingController(IMemberMeetingService memberMeetingService)
        {
            this.memberMeetingService = memberMeetingService;
        }

        [HttpGet]
        [Route("get/Meetings/{memberId}")]
        public async Task<IActionResult> GetMeetingsByMemberId(int memberId)
        {
            var meetings = await memberMeetingService.GetMeetingsByMemberId(memberId).ConfigureAwait(false);
            if (meetings != null)
            {
                return Ok(meetings);
            }

            return NotFound($"cant find meetings with the memberId: {memberId}");
        }

        [HttpGet]
        [Route("get/Members/{meetingsId}")]
        public async Task<IActionResult> GetMembersByMeetingsId(int meetingsId)
        {
            var members = await memberMeetingService.GetMembersByMeetingsId(meetingsId).ConfigureAwait(false);
            if (members != null)
            {
                return Ok(members);
            }

            return NotFound($"cant find members with the eventId: {meetingsId}");
        }

        [HttpPost]
        [Route("post/{memberId}/{meetingId}")]
        public async Task<IActionResult> CreateResponsibility(int memberId, int meetingId)
        {
          var memberMeeting = await memberMeetingService.AddMemberToMeeting(memberId, meetingId).ConfigureAwait(false);

            return Created("created successfully",
                memberMeeting);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteResponsibilityById(MemberMeeting memberMeeting)
        {
            if (memberMeeting != null)
            {
                await memberMeetingService.DeleteMemberFromMeeting(memberMeeting).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
