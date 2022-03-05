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
    [Route("api/Meeting")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService meetingService;
        private readonly IMapper _mapper;
        public MeetingController(IMeetingService meetingService, IMapper mapper)
        {
            this.meetingService = meetingService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetMeetings()
        {
            return Ok(await meetingService.GetMeetings().ConfigureAwait(false));
        }

        [HttpGet]
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

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> CreateMeeting(Meeting meeting)
        {
            await meetingService.AddMeeting(meeting).ConfigureAwait(false);

            var meetingDto = _mapper.Map<MeetingDto>(meeting);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + meetingDto.Id,
                meetingDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteMeetingById(int id)
        {
            var meeting = await meetingService.GetMeetingById(id).ConfigureAwait(false);
            if (meeting != null)
            {
                await meetingService.DeleteMeeting(meeting).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditMeeting(int id, Meeting meeting)
        {
            var existingMeeting = await meetingService.GetMeetingById(id).ConfigureAwait(false);
            if (existingMeeting != null)
            {
                meeting.Id = existingMeeting.Id;
                await meetingService.EditMeeting(meeting).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
