using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/Responsibility")]
    [ApiController]
    public class ResponsibilityController : ControllerBase
    {
        private readonly IResponsibilityService taskService;
        public ResponsibilityController(IResponsibilityService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetResponsibilitys()
        {
            return Ok(await taskService.GetResponsibilities().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetResponsibilityById(int id)
        {
            var task = await taskService.GetResponsibilityById(id).ConfigureAwait(false);
            if (task != null)
            {
                return Ok(task);
            }

            return NotFound($"cant find task with the id: {id}");
        }

        [HttpGet]
        [Route("get/responsibleId/{responsibleId}")]
        public async Task<IActionResult> GetResponsibilityByResponsibleId(int responsibleId)
        {
            var task = await taskService.GetResponsibilityByResponsibleId(responsibleId).ConfigureAwait(false);
            if (task != null)
            {
                return Ok(task);
            }

            return NotFound($"cant find task with the responsibleId: {responsibleId}");
        }

        [HttpGet]
        [Route("get/eventId/{eventId}")]
        public async Task<IActionResult> GetResponsibilityByEventId(int eventId)
        {
            var task = await taskService.GetResponsibilityByEventId(eventId).ConfigureAwait(false);
            if (task != null)
            {
                return Ok(task);
            }

            return NotFound($"cant find task with the eventId: {eventId}");
        }

        [HttpPost]
        [Route("post/{eventId}/{responsibleId}")]
        public async Task<IActionResult> CreateResponsibility(int eventId, int responsibleId, Responsibility task)
        {
            await taskService.AddResponsibility(eventId, responsibleId, task).ConfigureAwait(false);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + task.Id,
                task);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteResponsibilityById(int id)
        {
            var task = await taskService.GetResponsibilityById(id).ConfigureAwait(false);
            if (task != null)
            {
                await taskService.DeleteResponsibility(task).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditResponsibility(int id, Responsibility task)
        {
            var existingResponsibility = await taskService.GetResponsibilityById(id).ConfigureAwait(false);
            if (existingResponsibility != null)
            {
                task.Id = existingResponsibility.Id;
                await taskService.EditResponsibility(task).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
