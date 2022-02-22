using Licenta.Models;
using System;
using System.Collections.Generic;

namespace Licenta.Repository
{
    public interface IEventService
    { 
        List<Event> GetEvents();

        Event GetEventById(Guid id);

        Event AddEvent(Event _event);

        void DeleteEvent(Event _events);

        Event EditEvent(Event _event);
    }
}
