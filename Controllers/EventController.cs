using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Licenta.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService _eventService)
        {
            this._eventService = _eventService;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetEvents()
        {
            return Ok(_eventService.GetEvents());
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetEventById(Guid id)
        {
            var _event = _eventService.GetEventById(id);
            if (_event != null)
            {
                return Ok(_event);
            }

            return NotFound($"cant find _event with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public IActionResult CreateEvent(Event _event)
        {
            _eventService.AddEvent(_event);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + _event.Id,
                _event);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteEventById(Guid id)
        {
            var _event = _eventService.GetEventById(id);
            if (_event != null)
            {
                _eventService.DeleteEvent(_event);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public IActionResult EditEvent(Guid id, Event _event)
        {
            var existingEvent = _eventService.GetEventById(id);
            if (existingEvent != null)
            {
                _event.Id = existingEvent.Id;
                _eventService.EditEvent(_event);
                return Ok();
            }
            return NotFound();
        }
    }
}
