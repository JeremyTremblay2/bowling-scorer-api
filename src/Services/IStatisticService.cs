using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Interface that defines the operations that can be performed on the Statistics.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Retrieve a list of statistics based on the page number and number of statistics per page.
        /// </summary>
        /// <param name="page">The page number to retrieve</param>
        /// <param name="nbStatistics">The number of statistics per page</param>
        /// <returns>A list of Statistics objects</returns>
        Task<IEnumerable<Statistics>> GetAll(int page, int nbStatistics);

        /// <summary>
        /// Retrieve a specific Statistics object based on its id.
        /// </summary>
        /// <param name="id">The id of the Statistics object</param>
        /// <returns>A Statistics object</returns>
        Task<Statistics> GetById(int id);

        /// <summary>
        /// Add a new Statistics object to the data store.
        /// </summary>
        /// <param name="statistics">The Statistics object to be added</param>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
        Task<bool> AddStatistics(Statistics statistics);

        /// <summary>
        /// Edit an existing Statistics object in the data store.
        /// </summary>
        /// <param name="statistics">The Statistics object to be edited</param>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
        Task<bool> EditStatistics(Statistics statistics);

        /// <summary>
        /// Remove an existing Statistics object from the data store.
        /// </summary>
        /// <param name="id">The id of the Statistics object to be removed</param>
        /// <returns>A boolean indicating whether the operation was successful or not</returns>
        Task<bool> RemoveStatistics(int id);
    }
}
