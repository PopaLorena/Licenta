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
        public IActionResult GetMembers()
        {
            return Ok(memberService.GetMembers());
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetMemberById(Guid id)
        {
            var member = memberService.GetMemberById(id);
            if (member != null)
            {
                return Ok(member);
            }

            return NotFound($"cant find member with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public IActionResult CreateMember(MemberModel member)
        {
            memberService.AddMember(member);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + member.Id,
                member);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteMemberById(Guid id)
        {
            var member = memberService.GetMemberById(id);
            if (member != null)
            {
                memberService.DeleteMember(member);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public IActionResult EditMember(Guid id, MemberModel member)
        {
            var existingMember = memberService.GetMemberById(id);
            if (existingMember != null)
            {
                member.Id = existingMember.Id;
                memberService.EditMember(member);
                return Ok();
            }
            return NotFound();
        }
    }
}
