using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class MemberTrainingController : ControllerBase
    {
        private readonly IMemberTrainingService memberTrainingService;
        private readonly IMapper _mapper;

        public MemberTrainingController(IMemberTrainingService memberTrainingService, IMapper mapper)
        {
            this.memberTrainingService = memberTrainingService;
            _mapper = mapper;
        }

        [HttpPost, Authorize(Roles = "User,Admin")]
        [Route("post/{memberId}/{trainingId}")]
        public async Task<IActionResult> CreateMemberTraining(int memberId, int trainingId)
        {
            var memberTraining = await memberTrainingService.AddMemberToTraining(memberId, trainingId).ConfigureAwait(false);

            return Created("created successfully",
                memberTraining);
        }

        [HttpDelete, Authorize(Roles = "User,Admin")]
        [Route("delete/{memberId}/{trainingId}")]
        public async Task<IActionResult> DeleteMemberTrainingById(int memberId, int trainingId)
        {
            // var memberTraining = _mapper.Map<MemberTraining>(memberTrainingDto);
            await memberTrainingService.DeleteMemberFromTraining(memberId, trainingId).ConfigureAwait(false);
            return Ok();
        }
    }
}
