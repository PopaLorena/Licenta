using Licenta.Models;
using Licenta.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class EventService : IEventService
    {
        private readonly Context.ContextDb _context;
        //private readonly IResponsibilityService responsibilityService;
        public EventService(Context.ContextDb context)
        {
            _context = context;
            // this.responsibilityService = responsibilityService;
        }
        public async Task<Event> AddEvent(Event _event)
        {
            _context.Events.Add(_event);
            _context.SaveChanges();
            return _event;
        }

        public async Task DeleteEvent(Event _event)
        {
            //   var resp = await responsibilityService.GetResponsibilityByEventId(_event.Id).ConfigureAwait(false);
            //   foreach (Responsibility responsabilty in resp)
            // {
            //    await responsibilityService.DeleteResponsibility(responsabilty).ConfigureAwait(false);
            // }
            _context.Events.Remove(_event);
            _context.SaveChanges();
        }

        public async Task EditEvent(Event _event)
        {
            var existingEvent = _context.Events.Find(_event.Id);
            if (existingEvent != null)
            {

                existingEvent.Name = _event.Name;
                existingEvent.StartDate = _event.StartDate;
                existingEvent.Responsibilities = _event.Responsibilities;

                if (existingEvent.EndDate != _event.EndDate)
                {
                    foreach (Responsibility responsabilty in existingEvent.Responsibilities)
                    {
                        responsabilty.EndDate = _event.EndDate;
                        _context.Responsibilities.Update(responsabilty);
                    }
                    existingEvent.EndDate = _event.EndDate;
                }

                _context.Events.Update(existingEvent);
                _context.SaveChanges();
            }
        }

        public async Task<Event> GetEventById(int id)
        {
            return _context.Events.SingleOrDefault(x => x.Id == id);
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _context.Events.Include(e => e.Responsibilities).ToListAsync();
        }
    }
}
