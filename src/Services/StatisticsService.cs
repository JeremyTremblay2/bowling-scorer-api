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
    /// <summary>
    /// Class implementing the interface that defines the operations that can be performed on the Statistics.
    /// </summary>
    public class StatisticsService : IStatisticService
    {
        /// <summary>
        /// The repository used by this service.
        /// </summary>
        private readonly IStatisticRepository _statisticsRepository;

        /// <summary>
        /// Create a new instance of StatisticsService.
        /// </summary>
        /// <param name="statisticRepository">The repository used by this service.</param>
        public StatisticsService(IStatisticRepository statisticRepository)
        {
            _statisticsRepository = statisticRepository;
        }

        /// <summary>
        /// Retrieve a list of statistics based on the page number and number of statistics per page.
        /// </summary>
        /// <param name="page">The page number to retrieve</param>
        /// <param name="nbStatistics">The number of statistics per page</param>
        /// <returns>A list of Statistics objects</returns>
        public async Task<IEnumerable<Statistics>> GetAll(int page, int nbStatistics)
        {
            return await _statisticsRepository.GetAll(page, nbStatistics);
        }

        /// <summary>
        /// Retrieve a specific Statistics object based on its id.
        /// </summary>
        /// <param name="id">The id of the Statistics object</param>
        /// <returns>A Statistics object</returns>
        public async Task<Statistics> GetById(int id)
        {
            Statistics? stats = await _statisticsRepository.GetById(id);
            if (stats is null)
            {
                throw new StatisticsNotFoundException("This statistics doesn't exists.");
            }
            return stats;
        }

        /// <summary>
        /// Add a new Statistics object to the data store.
        /// </summary>
        /// <param name="statistics">The Statistics object to be added</param>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
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

        /// <summary>
        /// Edit an existing Statistics object in the data store.
        /// </summary>
        /// <param name="statistics">The Statistics object to be edited</param>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
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

        /// <summary>
        /// Remove an existing Statistics object from the data store.
        /// </summary>
        /// <param name="id">The id of the Statistics object to be removed</param>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
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
