using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/Responsibility")]
    [ApiController]
    public class ResponsibilityController : ControllerBase
    {
        private readonly IResponsibilityService responsabilityService;
        private readonly IMapper _mapper;

        public ResponsibilityController(IResponsibilityService responsabilityService, IMapper mapper)
        {
            this.responsabilityService = responsabilityService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetResponsibilitys()
        {
            return Ok(await responsabilityService.GetResponsibilities().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetResponsibilityById(int id)
        {
            var responsability = await responsabilityService.GetResponsibilityById(id).ConfigureAwait(false);
            if (responsability != null)
            {
                return Ok(responsability);
            }

            return NotFound($"cant find responsability with the id: {id}");
        }

        [HttpGet]
        [Route("get/responsibleId/{responsibleId}")]
        public async Task<IActionResult> GetResponsibilityByResponsibleId(int responsibleId)
        {
            var responsability = await responsabilityService.GetResponsibilityByResponsibleId(responsibleId).ConfigureAwait(false);
            if (responsability != null)
            {
                return Ok(responsability);
            }

            return NotFound($"cant find responsability with the responsibleId: {responsibleId}");
        }

        [HttpGet]
        [Route("get/eventId/{eventId}")]
        public async Task<IActionResult> GetResponsibilityByEventId(int eventId)
        {
            var responsability = await responsabilityService.GetResponsibilityByEventId(eventId).ConfigureAwait(false);
            if (responsability != null)
            {
                return Ok(responsability);
            }

            return NotFound($"cant find responsability with the eventId: {eventId}");
        }

        [HttpPost]
        [Route("post/{eventId}/{responsibleId}")]
        public async Task<IActionResult> CreateResponsibility(int eventId, int responsibleId, Responsibility responsability)
        {
            await responsabilityService.AddResponsibility(eventId, responsibleId, responsability).ConfigureAwait(false);

            var responsabilityDto = _mapper.Map<ResponsabilityDto>(responsability);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + responsabilityDto.Id,
                responsabilityDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteResponsibilityById(int id)
        {
            var responsability = await responsabilityService.GetResponsibilityById(id).ConfigureAwait(false);
            if (responsability != null)
            {
                await responsabilityService.DeleteResponsibility(responsability).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditResponsibility(int id, Responsibility responsability)
        {
            var existingResponsibility = await responsabilityService.GetResponsibilityById(id).ConfigureAwait(false);
            if (existingResponsibility != null)
            {
                responsability.Id = existingResponsibility.Id;
                await responsabilityService.EditResponsibility(responsability).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
