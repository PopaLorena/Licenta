using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Licenta.Repository
{
    public interface ITaskService
    {
        List<Task> GetTasks();

        Task GetTaskById(Guid id);

        Task AddTask(Task task);

        void DeleteTask(Task task);

        Task EditTask(Task task);
    }
}
