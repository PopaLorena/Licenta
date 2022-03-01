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
        public MemberTrainingController(IMemberTrainingService memberTrainingService)
        {
            this.memberTrainingService = memberTrainingService;
        }

        [HttpGet]
        [Route("get/Trainings/{memberId}")]
        public async Task<IActionResult> GetTrainingsByMemberId(int memberId)
        {
            var trainings = await memberTrainingService.GetTrainingsByMemberId(memberId).ConfigureAwait(false);
            if (trainings != null)
            {
                return Ok(trainings);
            }

            return NotFound($"cant find trainings with the memberId: {memberId}");
        }

        [HttpGet]
        [Route("get/Members/{trainingsId}")]
        public async Task<IActionResult> GetMembersByTrainingsId(int trainingsId)
        {
            var members = await memberTrainingService.GetMembersByTrainingId(trainingsId).ConfigureAwait(false);
            if (members != null)
            {
                return Ok(members);
            }

            return NotFound($"cant find members with the eventId: {trainingsId}");
        }

        [HttpPost]
        [Route("post/{memberId}/{trainingId}")]
        public async Task<IActionResult> CreateResponsibility(int memberId, int trainingId)
        {
            var memberTraining = await memberTrainingService.AddMemberToTraining(memberId, trainingId).ConfigureAwait(false);

            return Created("created successfully",
                memberTraining);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteResponsibilityById(MemberTraining memberTraining)
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
