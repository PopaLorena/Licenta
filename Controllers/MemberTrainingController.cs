using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/MemberTraining")]
    [ApiController]
    public class MemberTrainingController : ControllerBase
    {
        private readonly IMemberTrainingService memberTrainingService;
        private readonly IMapper _mapper;

        public MemberTrainingController(IMemberTrainingService memberTrainingService, IMapper mapper)
        {
            this.memberTrainingService = memberTrainingService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("post/{memberId}/{trainingId}")]
        public async Task<IActionResult> CreateMemberTraining(int memberId, int trainingId)
        {
            var memberTraining = await memberTrainingService.AddMemberToTraining(memberId, trainingId).ConfigureAwait(false);
    
            return Created("created successfully",
                memberTraining);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteMemberTrainingById(MemberTraining memberTraining)
        {
            if (memberTraining != null)
            {
                await memberTrainingService.DeleteMemberFromTraining(memberTraining).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
