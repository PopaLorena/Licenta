using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class MemberMeetingController : ControllerBase
    {
        private readonly IMemberMeetingService memberMeetingService;
        private readonly IMapper _mapper;

        public MemberMeetingController(IMemberMeetingService memberMeetingService, IMapper mapper)
        {
            this.memberMeetingService = memberMeetingService;
        }

        [HttpPost, Authorize(Roles = "User,Admin")]
        [Route("post/{memberId}/{meetingId}")]
        public async Task<IActionResult> CreateMemberMeeting(int memberId, int meetingId, MemberMeeting memberMeeting)
        {
            memberMeeting = await memberMeetingService.AddMemberToMeeting(memberId, meetingId).ConfigureAwait(false);
            return Created("Created", memberMeeting);
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get/byMemberId/{memberId}")]
        public async Task<IActionResult> GetMeetingsByMemberId(int memberId)
        {
            return Ok(await memberMeetingService.GetMeetingsByMemberId(memberId).ConfigureAwait(false));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get/byMeetingId/{meetingId}")]
        public async Task<IActionResult> GetMembersByMeetingId(int meetingId)
        {
            return Ok(await memberMeetingService.GetMembersByMeetingsId(meetingId).ConfigureAwait(false));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("Check/{memberId}/{meetingId}")]
        public async Task<IActionResult> CheckIfExist(int memberId, int meetingId)
        {
            return Ok(await memberMeetingService.CheckIfExist(memberId, meetingId).ConfigureAwait(false));
        }

        [HttpDelete, Authorize(Roles = "User,Admin")]
        [Route("delete/{memberId}/{meetingId}")]
        public async Task<IActionResult> DeleteMemberMeetingById(int memberId, int meetingId)
        {
            // var memberMeeting = _mapper.Map<MemberMeeting>(memberMeetingDto);
            await memberMeetingService.DeleteMemberFromMeeting(memberId, meetingId).ConfigureAwait(false);
            return Ok();
        }
    }
}
