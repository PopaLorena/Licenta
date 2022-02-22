using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Licenta.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetTasks()
        {
            return Ok(taskService.GetTasks());
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetTaskById(Guid id)
        {
            var task = taskService.GetTaskById(id);
            if (task != null)
            {
                return Ok(task);
            }

            return NotFound($"cant find task with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public IActionResult CreateTask(Task task)
        {
            taskService.AddTask(task);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + task.Id,
                task);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteTaskById(Guid id)
        {
            var task = taskService.GetTaskById(id);
            if (task != null)
            {
                taskService.DeleteTask(task);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public IActionResult EditTask(Guid id, Task task)
        {
            var existingTask = taskService.GetTaskById(id);
            if (existingTask != null)
            {
                task.Id = existingTask.Id;
                taskService.EditTask(task);
                return Ok();
            }
            return NotFound();
        }
    }
}
