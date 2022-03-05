using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface IEventService
    {
        Task<List<Event>> GetEvents();

        Task<Event> GetEventById(int id);

        Task<Event> AddEvent(Event _event);

        Task DeleteEvent(Event _event);

        Task EditEvent(Event _event);
    }
}
