using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface ITrainingService
    {
        Task<List<Training>> GetTrainings();

        Task<List<Training>> GetSortTrainings();

        Task<List<Training>> GetTrainingByMemberId(int id);

        Task<Training> GetTrainingById(int id);

        Task<List<MemberModel>> GetParicipants(int id);

        Task<Training> AddTraining(Training training);

        Task DeleteTraining(Training training);

        Task<Training> EditTraining(Training training);
    }
}
