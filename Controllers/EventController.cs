using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
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
        private readonly IMapper _mapper;

        public EventController(IEventService _eventService, IMapper mapper)
        {
            this._eventService = _eventService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _eventService.GetEvents().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var _event =  await _eventService.GetEventById(id).ConfigureAwait(false);
            if (_event != null)
            {
                return Ok(_event);
            }

            return NotFound($"cant find _event with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> CreateEvent(Event _event)
        {
            await _eventService.AddEvent(_event).ConfigureAwait(false);

            var eventDto = _mapper.Map<EventDto>(_event);
          
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + eventDto.Id,
                eventDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteEventById(int id)
        {
            var _event = await _eventService.GetEventById(id).ConfigureAwait(false);
            if (_event != null)
            {
               await _eventService.DeleteEvent(_event).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
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
            return NotFound();
        }
    }
}
