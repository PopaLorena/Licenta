using Licenta.Models;
using Licenta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Licenta.Services
{
    public class EventService : IEventService
    {
        private Context.ContextDb _context;
        public EventService(Context.ContextDb context)
        {
            _context = context;
        }
        public Event AddEvent(Event _event)
        {
            _event.Id = Guid.NewGuid();
            _context.Events.Add(_event);
            _context.SaveChanges();
            return _event;
        }

        public void DeleteEvent(Event _event)
        {
            _context.Events.Remove(_event);
            _context.SaveChanges();
        }

        public Event EditEvent(Event _event)
        {
            var existingEvent = _context.Events.Find(_event.Id);
            if (existingEvent != null)
            {
                existingEvent.Name = _event.Name;
                existingEvent.EndDate = _event.EndDate;
                existingEvent.StartDate = _event.StartDate;

                _context.Events.Update(existingEvent);
                _context.SaveChanges();
            }
            return _event;
        }

        public Event GetEventById(Guid id)
        {
            return _context.Events.SingleOrDefault(x => x.Id == id);
        }

        public List<Event> GetEvents()
        {
            return _context.Events.ToList();
        }
    }
}
