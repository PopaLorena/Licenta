using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface IMemberTrainingService
    {
        Task<List<Training>> GetTrainingsByMemberId(int memberId);
        Task<List<MemberModel>> GetMembersByTrainingId(int trainingId);

        Task<MemberTraining> AddMemberToTraining(int memberId, int trainingId);

        Task DeleteMemberFromTraining(MemberTraining memberTraining);
    }
}
