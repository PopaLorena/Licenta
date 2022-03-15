using Licenta.Models;
using Licenta.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class ResponsibilityService : IResponsibilityService
    {
        private readonly Context.ContextDb _context;
        private readonly IEventService _eventService;
        private readonly IMemberService memberService;
        public ResponsibilityService(Context.ContextDb context, IEventService _eventService, IMemberService memberService)
        {
            _context = context;
            this._eventService = _eventService;
            this.memberService = memberService;
        }
        public async Task<Responsibility> AddResponsibility(int eventId, int responsibleId, Responsibility task)
        {
            var _event = await _eventService.GetEventById(eventId).ConfigureAwait(false);
            task.EventId = eventId;
            task.ResponsibleId = responsibleId;

            task.Responsible = await memberService.GetMemberById(responsibleId).ConfigureAwait(false);
            task.Event = _event;
            task.EndDate = task.Event.EndDate;
            _context.Responsibilities.Add(task);
            _context.SaveChanges();

            return task;
        }

        public async Task DeleteResponsibility(Responsibility task)
        {
            _context.Responsibilities.Remove(task);
            _context.SaveChanges();
        }

        public async Task<Responsibility> EditResponsibility(Responsibility task)
        {
            var existingResponsibility = _context.Responsibilities.Find(task.Id);
            if (existingResponsibility != null)
            {
                existingResponsibility.Name = task.Name;
                existingResponsibility.Description = task.Description;
                existingResponsibility.EventId = task.EventId;
                existingResponsibility.ResponsibleId = task.ResponsibleId;
                existingResponsibility.StartDate = task.StartDate;

                _context.Responsibilities.Update(existingResponsibility);
                _context.SaveChanges();
            }
            return task;
        }

        public async Task<Responsibility> GetResponsibilityById(int id)
        {
            return _context.Responsibilities.SingleOrDefault(x => x.Id == id);
        }

        public async Task<List<Responsibility>> GetResponsibilities()
        {
            return _context.Responsibilities.ToList();
        }

        public async Task<List<Responsibility>> GetResponsibilityByResponsibleId(int responsibleId)
        {
            var resp = from r in _context.Responsibilities
                       where (r.ResponsibleId == responsibleId)
                       select r;
            return resp.ToList();
        }

        public async Task<List<Responsibility>> GetResponsibilityByEventId(int eventId)
        {
            var resp = from r in _context.Responsibilities
                       where (r.EventId == eventId)
                       select r;
            return resp.ToList();
        }
    }
}
