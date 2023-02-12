using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IStatisticService
    {
        Task<IEnumerable<Statistics>> GetAll(int page, int nbStatistics);

        Task<Statistics?> GetById(int id);

        Task<bool> AddStatistics(Statistics statistics);

        Task<bool> EditStatistics(Statistics statistics);

        Task<bool> RemoveStatistics(int id);
    }
}
