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
        public MeetingController(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetMeetings()
        {
            return Ok(meetingService.GetMeetings());
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetMeetingById(Guid id)
        {
            var meeting = meetingService.GetMeetingById(id);
            if (meeting != null)
            {
                return Ok(meeting);
            }

            return NotFound($"cant find meeting with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public IActionResult CreateMeeting(Meeting meeting)
        {
            meetingService.AddMeeting(meeting);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + meeting.Id,
                meeting);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteMeetingById(Guid id)
        {
            var meeting = meetingService.GetMeetingById(id);
            if (meeting != null)
            {
                meetingService.DeleteMeeting(meeting);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public IActionResult EditMeeting(Guid id, Meeting meeting)
        {
            var existingMeeting = meetingService.GetMeetingById(id);
            if (existingMeeting != null)
            {
                meeting.Id = existingMeeting.Id;
                meetingService.EditMeeting(meeting);
                return Ok();
            }
            return NotFound();
        }
    }
}
