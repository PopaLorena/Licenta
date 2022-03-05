using AutoMapper;
using Licenta.Dto;
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
        private readonly IMapper _mapper;

        public MemberMeetingController(IMemberMeetingService memberMeetingService, IMapper mapper)
        {
            this.memberMeetingService = memberMeetingService;
        }

        [HttpPost]
        [Route("post/{memberId}/{meetingId}")]
        public async Task<IActionResult> CreateMemberMeeting(int memberId, int meetingId)
        {
            var memberMeeting = await memberMeetingService.AddMemberToMeeting(memberId, meetingId).ConfigureAwait(false);
            return Created("Created", memberMeeting);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteMemberMeetingById(MemberMeeting memberMeeting)
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
