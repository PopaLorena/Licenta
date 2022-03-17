﻿using AutoMapper;
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
   // [EnableCors("MyPolicy")]
    //[Authorize]
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

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("post")]
        public async Task<IActionResult> CreateMeeting(Meeting meeting)
        {

            //var meeting = _mapper.Map<Meeting>(meetingDto);
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
            return NotFound();
        }

        [HttpPatch, Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditMeeting(int id, Meeting meeting)
        {
          //  var meeting = _mapper.Map<Meeting>(meetingDto);
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
