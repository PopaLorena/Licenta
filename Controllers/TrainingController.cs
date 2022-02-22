using Licenta.Models;
using Licenta.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Licenta.Controllers
{
    [Route("api/Training")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService trainingService;
        public TrainingController(ITrainingService trainingService)
        {
            this.trainingService = trainingService;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetTrainings()
        {
            return Ok(trainingService.GetTrainings());
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetTrainingById(Guid id)
        {
            var training = trainingService.GetTrainingById(id);
            if (training != null)
            {
                return Ok(training);
            }

            return NotFound($"cant find training with the id: {id}");
        }

        [HttpPost]
        [Route("post")]
        public IActionResult CreateTraining(Training training)
        {
            trainingService.AddTraining(training);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + training.Id,
                training);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteTrainingById(Guid id)
        {
            var training = trainingService.GetTrainingById(id);
            if (training != null)
            {
                trainingService.DeleteTraining(training);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("edit/{id}")]
        public IActionResult EditTraining(Guid id, Training training)
        {
            var existingTraining = trainingService.GetTrainingById(id);
            if (existingTraining != null)
            {
                training.Id = existingTraining.Id;
                trainingService.EditTraining(training);
                return Ok();
            }
            return NotFound();
        }
    }
}
