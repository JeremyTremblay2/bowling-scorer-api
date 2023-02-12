using Model;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Interface representing a rpeository for the statistics.
    /// </summary>
    public interface IStatisticRepository
    {
        /// <summary>
        /// Return all the statistics in the DB
        /// </summary>
        /// <returns>All statistics in the DB</returns>
        Task<IEnumerable<Statistics>> GetAll(int page, int nbStatistics);

        /// <summary>
        /// Get the first statistics specified from the stated index ordered by the ID.
        /// </summary>
        /// <param name="index">The index to get the first statistics.</param>
        /// <param name="count">The number of statistics to get.</param>
        /// <returns>The collection of statistics retrieve.</returns>
        Task<IEnumerable<Statistics>> GetStatistics(int index, int count);

        /// <summary>
        /// Return the speficied statistics if he exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>statistics if he exists, null if he doesn't exists</returns>
        Task<Statistics?> GetById(int id);

        /// <summary>
        /// Add a statistics to the DB.
        /// </summary>
        /// <param name="statistics">The statistics to add.</param>
        /// <returns>A boolean indicating if the statistics was added.</returns>
        Task<bool> AddStatistics(Statistics statistics);

        /// <summary>
        /// Change the name and the image of the specified statistics.
        /// </summary>
        /// <param name="statistics">The statistics to edit.</param>
        /// <returns>A boolean indicating if the statistics was succesfully updated.</returns>
        Task<bool> EditStatistics(Statistics statistics);

        /// <summary>
        /// Remove a statistics from the manager.
        /// </summary>
        /// <param name="id">Id of the statistics to remove</param>
        /// <returns>A boolean indicating if the statistics was removed.</returns>
        Task<bool> RemoveStatistics(int id);
    }
}
