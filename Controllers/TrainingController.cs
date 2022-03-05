using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/Training")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService trainingService;
        private readonly IMapper _mapper;

        public TrainingController(ITrainingService trainingService, IMapper mapper)
        {
            this.trainingService = trainingService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetTrainings()
        {
            return Ok(await trainingService.GetTrainings().ConfigureAwait(false));
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetTrainingById(int id)
        {
            var training = await trainingService.GetTrainingById(id).ConfigureAwait(false);
            if (training != null)
            {
                return Ok(training);
            }

            return NotFound($"cant find training with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> CreateTraining(Training training)
        {
            await trainingService.AddTraining(training).ConfigureAwait(false);

            var trainingDto = _mapper.Map<TrainingDto>(training);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + trainingDto.Id,
                trainingDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTrainingById(int id)
        {
            var training = await trainingService.GetTrainingById(id).ConfigureAwait(false);
            if (training != null)
            {
                await trainingService.DeleteTraining(training).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTraining(int id, Training training)
        {
            var existingTraining = await trainingService.GetTrainingById(id).ConfigureAwait(false);
            if (existingTraining != null)
            {
                training.Id = existingTraining.Id;
                await trainingService.EditTraining(training).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }
    }
}
