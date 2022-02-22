using Licenta.Models;
using Licenta.Repository;
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

        public Training AddTraining(Training training)
        {
            training.Id = Guid.NewGuid();
            _context.Trainings.Add(training);
            _context.SaveChanges();
            return training;
        }

        public void DeleteTraining(Training training)
        {
            _context.Trainings.Remove(training);
            _context.SaveChanges();
        }

        public Training EditTraining(Training training)
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

        public Training GetTrainingById(Guid id)
        {
            return _context.Trainings.SingleOrDefault(x => x.Id == id);
        }

        public List<Training> GetTrainings()
        {
            return _context.Trainings.ToList();
        }

        public Training AddParticipants(Guid id, MemberModel member)
        {
            var existingTraining = _context.Trainings.SingleOrDefault(x => x.Id == id);
            existingTraining.Participants.Add(member);
            _context.Trainings.Update(existingTraining);

            AddTrainingToMember(existingTraining, member.Id);

            _context.SaveChanges();

            return existingTraining;
        }

        private void AddTrainingToMember(Training training, Guid memberId)
        {
            MemberModel member = memberService.GetMemberById(memberId);

            member.Trainings.Add(training);
            memberService.EditMember(member);
        }
    }
}
