using AutoMapper;
using Licenta.Context;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ContextDb _context;

        public EventController(IEventService _eventService, ContextDb context)
        {
            this._eventService = _eventService;
            _context = context;
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
        public async Task<IActionResult> CreateEvent(Event _event)
        {
            try
            {
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
        public async Task<IActionResult> EditEvent(int id, Event _event)
        {
            var existingEvent = await _eventService.GetEventById(id).ConfigureAwait(false);
            if (existingEvent != null)
            {
                _event.Id = existingEvent.Id;
                await _eventService.EditEvent(_event).ConfigureAwait(false);
                return Ok();
            }
            return NotFound($"cant find the event with the id: {id}");
        }
    }
}
