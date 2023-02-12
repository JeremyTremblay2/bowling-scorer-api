using DTOs;
using Model.Players;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StatisticsService : IStatisticService
    {
        private readonly IStatisticRepository _statisticsRepository;

        public StatisticsService(IStatisticRepository statisticRepository)
        {
            _statisticsRepository = statisticRepository;
        }

        public async Task<IEnumerable<Statistics>> GetAll(int page, int nbStatistics)
        {
            return await _statisticsRepository.GetAll(page, nbStatistics);
        }

        public async Task<Statistics> GetById(int id)
        {
            Statistics? stats = await _statisticsRepository.GetById(id);
            if (stats is null)
            {
                throw new StatisticsNotFoundException("This statistics doesn't exists.");
            }
            return stats;
        }

        public async Task<bool> AddStatistics(Statistics statistics)
        {
            if (GetById(statistics.ID) is null)
            {
                throw new StatisticsAlreadyExistsException("Some statistics already exist with the same ID");
            }
            else if (statistics.NumberOfGames < 0 || statistics.NumberOfDefeat > statistics.NumberOfGames
                || statistics.NumberOfVictory > statistics.NumberOfGames
                || (statistics.NumberOfDefeat + statistics.NumberOfVictory) > statistics.NumberOfGames)
            {
                throw new StatisticsNotValidException("The number of games cannot be grater than the number of victory, the number of defeat or the two together.");
            }
            else if (statistics.BestScore < 0 || statistics.BestScore > 300)
            {
                throw new StatisticsNotValidException("The best score cannot be greater than 300 and must be greater or equals than 0.");
            }
            return await _statisticsRepository.AddStatistics(statistics);
        }

        public async Task<bool> EditStatistics(Statistics statistics)
        {
            Statistics? stats = await GetById(statistics.ID);
            if (stats is null)
            {
                throw new StatisticsNotFoundException("The player that you want to edit doesn't exists");
            }
            else if (stats.Equals(statistics))
            {
                return true;
            }
            else if (statistics.NumberOfGames < 0 || statistics.NumberOfDefeat > statistics.NumberOfGames 
                || statistics.NumberOfVictory > statistics.NumberOfGames 
                || (statistics.NumberOfDefeat + statistics.NumberOfVictory) > statistics.NumberOfGames)
            {
                throw new StatisticsNotValidException("The number of games cannot be grater than the number of victory, the number of defeat or the two together.");
            }
            else if (statistics.BestScore < 0 || statistics.BestScore > 300)
            {
                throw new StatisticsNotValidException("The best score cannot be greater than 300 and must be greater or equals than 0.");
            }
            else
            {
                return await _statisticsRepository.EditStatistics(statistics);
            }
        }

        public async Task<bool> RemoveStatistics(int id)
        {
            if (GetById(id) is null)
            {
                throw new StatisticsNotFoundException("The statistics that you want to delete doesn't exists");
            }
            return await _statisticsRepository.RemoveStatistics(id);
        }
    }
}
