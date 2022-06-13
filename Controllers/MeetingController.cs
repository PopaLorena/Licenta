using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/Meeting")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService meetingService;
        private readonly IMapper mapper;

        public MeetingController(IMeetingService meetingService, IMapper mapper)
        {
            this.meetingService = meetingService ?? throw new ArgumentNullException(nameof(meetingService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetMeetings()
        {
            return Ok(await meetingService.GetMeetings().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("getSort")]
        public async Task<IActionResult> GetSortActiveMeetings()
        {
            return Ok(await meetingService.GetSortMeetings().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("getNext")]
        public async Task<IActionResult> GetYourNextMeeting()
        {
            return Ok(await meetingService.GetNextMeeting().ConfigureAwait(false));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get/{id}")]
        public async Task<IActionResult> GetMeetingById(int id)
        {
            var meeting = await meetingService.GetMeetingById(id).ConfigureAwait(false);
            if (meeting != null)
            {
                return Ok(meeting);
            }

            return NotFound($"cant find meeting with the id: {id}");
        }

        [HttpGet]
        [Route("getByMemberId/{id}")]
        public async Task<IActionResult> GetMeetingByMemberId(int id)
        {
            var meeting = await meetingService.GetMeetingByMemberId(id).ConfigureAwait(false);
            if (meeting != null)
            {
                return Ok(meeting);
            }

            return NotFound($"cant find any meetings with the MemberId: {id}");
        }

        [HttpGet]
        [Route("getParticipants/{id}")]
        public async Task<IActionResult> GetParicipants(int id)
        {
            var members = await meetingService.GetParicipants(id).ConfigureAwait(false);
            if (members != null)
            {
                return Ok(members);
            }

            return NotFound($"cant find any paricipant to the meeting with the Id: {id}");
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("post")]
        public async Task<IActionResult> CreateMeeting(MeetingDto meetingDto)
        {
            var meeting = mapper.Map<Meeting>(meetingDto);
            await meetingService.AddMeeting(meeting).ConfigureAwait(false);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + meeting.Id,
                meeting);
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteMeetingById(int id)
        {
            var meeting = await meetingService.GetMeetingById(id).ConfigureAwait(false);
            if (meeting != null)
            {
                await meetingService.DeleteMeeting(meeting).ConfigureAwait(false);
                return Ok();
            }
            return NotFound($"cant find the meeting with the id: {id}");
        }

        [HttpPatch, Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditMeeting(int id, MeetingDto meetingDto)
        {
            var existingMeeting = await meetingService.GetMeetingById(id).ConfigureAwait(false);
            if (existingMeeting != null)
            {
                var meeting = mapper.Map<Meeting>(meetingDto);
                meeting.Id = existingMeeting.Id;
                await meetingService.EditMeeting(meeting).ConfigureAwait(false);
                return Ok();
            }
            return NotFound($"cant find the meeting with the id: {id}");
        }
    }
}
