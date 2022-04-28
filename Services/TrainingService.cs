using Licenta.Models;
using Licenta.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly Context.ContextDb _context;
        private readonly IMemberService memberService;
        public TrainingService(Context.ContextDb context, IMemberService memberService)
        {
            _context = context;
            this.memberService = memberService;
        }

        public async Task<Training> AddTraining(Training training)
        {
            _context.Trainings.Add(training);
            _context.SaveChanges();
            return training;
        }

        public async Task DeleteTraining(Training training)
        {
            _context.Trainings.Remove(training);
            _context.SaveChanges();
        }

        public async Task<Training> EditTraining(Training training)
        {
            var existingTraining = _context.Trainings.Find(training.Id);
            if (existingTraining != null)
            {
                existingTraining.Name = training.Name;
                existingTraining.Date = training.Date;
                existingTraining.Participants = training.Participants;
                existingTraining.TrainerName = training.TrainerName;

                _context.Trainings.Update(existingTraining);
                _context.SaveChanges();
            }
            return training;
        }

        public async Task<Training> GetTrainingById(int id)
        {
            return _context.Trainings.SingleOrDefault(x => x.Id == id);
        }

        public async Task<List<Training>> GetTrainings()
        {
            return _context.Trainings.Include(t => t.Participants).ToList();
        }

        public async Task<List<Training>> GetSortTrainings()
        {
            return _context.Trainings.OrderBy(t => t.Date).Where(m => DateTime.Compare(m.Date, DateTime.Now) > 0).Include(m => m.Participants).ToList();
        }
    }
}
