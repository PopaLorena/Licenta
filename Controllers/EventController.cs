using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper mapper;

        public EventController(IEventService _eventService, IMapper mapper)
        {
            this._eventService = _eventService ?? throw new ArgumentNullException(nameof(_eventService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetEvents()
        {
            try 
            { 
                return Ok(await _eventService.GetEvents().ConfigureAwait(false)); 
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                var _event = await _eventService.GetEventById(id).ConfigureAwait(false);
                if (_event != null)
                {
                    return Ok(_event);
                }

                return NotFound($"cant find the event with the id: {id}");
            }
            catch 
            {
                return NotFound($"cant find the event with the id: {id}");
            }
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("post")]
        public async Task<IActionResult> CreateEvent(EventDto _eventDto)
        {
            try
            {
                var _event = mapper.Map<Event>(_eventDto);
                await _eventService.AddEvent(_event).ConfigureAwait(false);

                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + _event.Id,
                    _event);
            }
            catch
            {
                return BadRequest("The request is not valid.");
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteEventById(int id)
        {
            var _event = await _eventService.GetEventById(id).ConfigureAwait(false);
            if (_event != null)
            {
               await _eventService.DeleteEvent(_event).ConfigureAwait(false);
                return Ok();
            }
            return NotFound($"cant find the event with the id: {id}");
        }

        [HttpPatch, Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditEvent(int id, EventDto _eventDto)
        {
            var existingEvent = await _eventService.GetEventById(id).ConfigureAwait(false);
            if (existingEvent != null)
            {
                var _event = mapper.Map<Event>(_eventDto);
                _event.Id = existingEvent.Id;
                await _eventService.EditEvent(_event).ConfigureAwait(false);
                return Ok();
            }
            return NotFound($"cant find the event with the id: {id}");
        }
    }
}
