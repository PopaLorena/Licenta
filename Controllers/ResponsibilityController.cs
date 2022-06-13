using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IResponsibilityService responsabilityService;
        private readonly IMapper mapper;

        public ResponsibilityController(IResponsibilityService responsabilityService, IMapper mapper)
        {
            this.responsabilityService = responsabilityService ?? throw new ArgumentNullException(nameof(responsabilityService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get")]
        public async Task<IActionResult> GetResponsibilities()
        {
            return Ok(await responsabilityService.GetResponsibilities().ConfigureAwait(false));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get/byEventId/{eventId}")]
        public async Task<IActionResult> GetResponsibilitiesByEventId(int eventId)
        {
            return Ok(await responsabilityService.GetResponsibilityByEventId(eventId).ConfigureAwait(false));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
        [Route("get/byMemberId/{memberId}")]
        public async Task<IActionResult> GetResponsibilitiesByMemberId(int memberId)
        {
            return Ok(await responsabilityService.GetResponsibilityByMemberId(memberId).ConfigureAwait(false));
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
        public async Task<IActionResult> CreateResponsibility(int eventId, int responsibleId, ResponsabilityDto responsabilityDto)
        {
            var responsability = mapper.Map<Responsibility>(responsabilityDto);
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
        public async Task<IActionResult> EditResponsibility(int id, ResponsabilityDto responsabilityDto)
        {
            var existingResponsibility = await responsabilityService.GetResponsibilityById(id).ConfigureAwait(false);
            if (existingResponsibility != null)
            {
                var responsability = mapper.Map<Responsibility>(responsabilityDto);
                responsability.Id = existingResponsibility.Id;
                await responsabilityService.EditResponsibility(responsability).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
