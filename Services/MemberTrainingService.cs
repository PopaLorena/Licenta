using Licenta.Models;
using Licenta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class MemberTrainingService : IMemberTrainingService
    {
        private readonly Context.ContextDb _context;
        private readonly ITrainingService trainingService;
        private readonly IMemberService memberService;
        public MemberTrainingService(Context.ContextDb context, ITrainingService trainingService, IMemberService memberService)
        {
            _context = context;
            this.trainingService = trainingService;
            this.memberService = memberService;
        }

        public async Task<MemberTraining> AddMemberToTraining(int memberId, int trainingId)
        {
            MemberTraining memberTraining = new MemberTraining();
            var member = await memberService.GetMemberById(memberId).ConfigureAwait(false);
            var training = await trainingService.GetTrainingById(trainingId).ConfigureAwait(false);

            memberTraining.TrainingId = trainingId;
            memberTraining.Member = member;
            memberTraining.MemberId = memberId;
            memberTraining.Training = training;

            _context.MemberTrainings.Add(memberTraining);
            _context.SaveChanges();

            return memberTraining;
        }

        public async Task DeleteMemberFromTraining(int memberId, int trainingId)
        {
            MemberTraining memberTraining = new MemberTraining();

            memberTraining = _context.MemberTrainings.Single(c => c.TrainingId == trainingId && c.MemberId == memberId);
          
            if (memberTraining.Member == null)
            {
                memberTraining.Member = await memberService.GetMemberById(memberTraining.MemberId).ConfigureAwait(false);
            }
            if (memberTraining.Training == null)
            {
                memberTraining.Training = await trainingService.GetTrainingById(memberTraining.TrainingId).ConfigureAwait(false);
            }
            _context.MemberTrainings.Remove(memberTraining);
            _context.SaveChanges();
        }

        public async Task<List<Training>> GetTrainingsByMemberId(int memberId)
        {
            var trainings = from m in _context.MemberTrainings
                           where (m.MemberId == memberId)
                           select m.Training;

            return trainings.ToList();
        }

        public async Task<List<MemberModel>> GetMembersByTrainingId(int trainingId)
        {
            var members = from m in _context.MemberTrainings
                          where (m.TrainingId == trainingId)
                          select m.Member;

            return members.ToList();
        }
    }
}
