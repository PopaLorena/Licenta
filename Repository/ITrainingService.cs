using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface ITrainingService
    {
        List<Training> GetTrainings();

        Training GetTrainingById(Guid id);

        Training AddTraining(Training training);

        void DeleteTraining(Training training);

        Training EditTraining(Training training);
    }
}
