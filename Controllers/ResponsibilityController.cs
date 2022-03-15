using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/Responsibility")]
    [ApiController]
    [Authorize]
    public class ResponsibilityController : ControllerBase
    {
        private readonly IResponsibilityService responsabilityService;
        private readonly IMapper _mapper;

        public ResponsibilityController(IResponsibilityService responsabilityService, IMapper mapper)
        {
            this.responsabilityService = responsabilityService;
            _mapper = mapper;
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get")]
        public async Task<IActionResult> GetResponsibilitys()
        {
            return Ok(await responsabilityService.GetResponsibilities().ConfigureAwait(false));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
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

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("post/{eventId}/{responsibleId}")]
        public async Task<IActionResult> CreateResponsibility(int eventId, int responsibleId, Responsibility responsability)
        {
          //  var responsability = _mapper.Map<Responsibility>(responsabilityDto);
            await responsabilityService.AddResponsibility(eventId, responsibleId, responsability).ConfigureAwait(false);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + responsability.Id,
                responsability);
        }

        [HttpDelete, Authorize(Roles = "Admin")]
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

        [HttpPatch, Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditResponsibility(int id, Responsibility responsability)
        {
          //  var responsability = _mapper.Map<Responsibility>(responsabilityDto);
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
