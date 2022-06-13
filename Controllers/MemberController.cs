using AutoMapper;
using Licenta.Context;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{

    [ApiController]
    [Route("api/Member")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;
        private readonly IMapper mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetMembers()
        {
            return Ok(await memberService.GetMembers().ConfigureAwait(false));
        }

        [HttpGet, Authorize(Roles = "User,Admin")]
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

        [HttpGet]
        [Route("get/byUsername/{username}")]
        public async Task<IActionResult> GetMemberByUsername(string username)
        {
            try
            {
                var member = await memberService.GetMemberByUsername(username).ConfigureAwait(false);

                if (member != null)
                {
                    return Ok(member.Id);
                }

                return NotFound($"cant find member with the username: {username}");
            }
            catch (Exception e)
            {
                return NotFound($"cant find member with the username: {username}");
            }
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("post")]
        public async Task<IActionResult> CreateMember(MemberModelDto memberDto)
        {
            var member = mapper.Map<MemberModel>(memberDto);
            var existingMember = memberService.GetMemberByEmail(member.Email);
            if (existingMember.Result != null)
                return BadRequest("Email is already taken");
            
           existingMember = memberService.GetMemberByTel(member.TelNumber);
            if (existingMember.Result != null)
                return BadRequest("Phnoe number is already taken");

            await memberService.AddMember(member).ConfigureAwait(false);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + member.Id,
                member);
        }

        [HttpDelete, Authorize(Roles = "Admin")]
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

        [HttpPatch, Authorize(Roles = "Admin")]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditMember(int id, MemberModelDto memberDto)
        {
            var existingMember = await memberService.GetMemberById(id).ConfigureAwait(false);
            var member = mapper.Map<MemberModel>(memberDto);
            if (existingMember.Email != member.Email)
            {
               var EMember = memberService.GetMemberByEmail(member.Email);
                if (EMember.Result != null)
                    return BadRequest("Email is already taken");
            }

            if (existingMember.TelNumber != member.TelNumber)
            {
                var TMember = memberService.GetMemberByTel(member.TelNumber);
                if (TMember.Result != null)
                    return BadRequest("Phnoe number is already taken");

            }
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
