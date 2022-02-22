using Licenta.Models;
using Licenta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Licenta.Services
{
    public class TaskService : ITaskService
    {
        private readonly Context.ContextDb _context;
        private readonly IEventService _eventService;
        private readonly IMemberService memberService;
        public TaskService(Context.ContextDb context, IEventService _eventService, IMemberService memberService)
        {
            _context = context;
            this._eventService = _eventService;
            this.memberService = memberService;
        }
        public Task AddTask(Task task)
        {
            task.Id = Guid.NewGuid();
            task.EndDate = _eventService.GetEventById(task.EventId).EndDate;

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public void DeleteTask(Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public Task EditTask(Task task)
        {
            var existingTask = _context.Tasks.Find(task.Id);
            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.Description = task.Description;
                existingTask.EndDate = task.EndDate;
                existingTask.EventId = task.EventId;
                existingTask.ResponsibleId = task.ResponsibleId;
                existingTask.StartDate = task.StartDate;

                _context.Tasks.Update(existingTask);
                _context.SaveChanges();
            }
            return task;
        }

        public Task GetTaskById(Guid id)
        {
            return _context.Tasks.SingleOrDefault(x => x.Id == id);
        }

        public List<Task> GetTasks()
        {
            return _context.Tasks.ToList();
        }
    }
}
