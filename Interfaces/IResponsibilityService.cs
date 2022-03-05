using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repository
{
    public interface IResponsibilityService
    {
        Task<List<Responsibility>> GetResponsibilities();

        Task<Responsibility> GetResponsibilityById(int id);

        Task<Responsibility> AddResponsibility(int eventId, int responsibleId, Responsibility task);

        Task DeleteResponsibility(Responsibility task);

        Task<Responsibility> EditResponsibility(Responsibility task);

        Task<List<Responsibility>> GetResponsibilityByResponsibleId(int responsibleId);

        Task<List<Responsibility>> GetResponsibilityByEventId(int eventId);
    }
}
