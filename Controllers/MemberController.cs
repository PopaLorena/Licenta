using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{

    [ApiController]
    [Route("api/Member")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;
        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetMembers()
        {
            return Ok(await memberService.GetMembers().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await memberService.GetMemberById(id).ConfigureAwait(false);
            if (member != null)
            {
                return Ok(member);
            }

            return NotFound($"cant find member with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> CreateMember(MemberModel member)
        {
            await memberService.AddMember(member).ConfigureAwait(false);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + member.Id,
                member);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteMemberById(int id)
        {
            var member = await memberService.GetMemberById(id).ConfigureAwait(false);
            if (member != null)
            {
                await memberService.DeleteMember(member).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditMember(int id, MemberModel member)
        {
            var existingMember = await memberService.GetMemberById(id).ConfigureAwait(false);
            if (existingMember != null)
            {
                member.Id = existingMember.Id;
                await memberService.EditMember(member).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
