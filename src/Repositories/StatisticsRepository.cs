using Entities;
using EntityToModel;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Class implementing the interface representing a rpeository for the statistics.
    /// </summary>
    public class StatisticsRepository : IStatisticRepository
    {
        /// <summary>
        /// Add a statistics to the DB.
        /// </summary>
        /// <param name="statistics">The statistics to add.</param>
        /// <returns>A boolean indicating if the statistics was added.</returns>
        public async Task<bool> AddStatistics(Statistics statistics)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Statistics is not null)
                {
                    await context.Statistics.AddAsync(statistics.ToEntity());
                    try
                    {
                        return await context.SaveChangesAsync() == 1;
                    }
                    catch (DbUpdateException e)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Change the name and the image of the specified statistics.
        /// </summary>
        /// <param name="statistics">The statistics to edit.</param>
        /// <returns>A boolean indicating if the statistics was succesfully updated.</returns>
        public async Task<bool> EditStatistics(Statistics statistics)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Statistics is not null)
                {
                    StatisticsEntity? se = await context.Statistics.FindAsync(statistics.ID);
                    if (se != null)
                    {
                        se.NumberOfVictory = statistics.NumberOfVictory;
                        se.NumberOfDefeat = statistics.NumberOfDefeat;
                        se.NumberOfGames = statistics.NumberOfGames;
                        se.BestScore = statistics.BestScore;
                        return await context.SaveChangesAsync() == 1;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Return all the statistics in the DB
        /// </summary>
        /// <returns>All statistics in the DB</returns>
        public async Task<IEnumerable<Statistics>> GetAll(int page, int nbStatistics)
        {
            using (BowlingDbContext context = new())
            {
                List<Statistics> statistics = new();
                if (context.Statistics is not null)
                {
                    statistics = await context.Statistics
                        .Skip(nbStatistics * page)
                        .Take(nbStatistics)
                        .Select(pl => pl.ToModel())
                        .ToListAsync(); // force the query
                }
                return statistics;
            }
        }

        /// <summary>
        /// Return the speficied statistics if he exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>statistics if he exists, null if he doesn't exists</returns>
        public async Task<Statistics?> GetById(int id)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Statistics is not null)
                {
                    StatisticsEntity? se = await context.Statistics.FindAsync(id);
                    if (se is not null)
                    {
                        return se.ToModel();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Get the first statistics specified from the stated index ordered by the ID.
        /// </summary>
        /// <param name="index">The index to get the first statistics.</param>
        /// <param name="count">The number of statistics to get.</param>
        /// <returns>The collection of statistics retrieve.</returns>
        public async Task<IEnumerable<Statistics>> GetStatistics(int index, int count)
        {
            using (BowlingDbContext context = new())
            {
                List<Statistics> statistics = new();
                if (context.Statistics is not null)
                {
                    statistics = await context.Statistics.Skip(index * count)
                        .Take(count)
                        .Select(pl => pl.ToModel())
                        .ToListAsync();
                }
                return statistics;
            }
        }

        /// <summary>
        /// Remove a statistics from the manager.
        /// </summary>
        /// <param name="id">Id of the statistics to remove</param>
        /// <returns>A boolean indicating if the statistics was removed.</returns>
        public async Task<bool> RemoveStatistics(int id)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Statistics is not null)
                {
                    StatisticsEntity? se = await context.Statistics.FindAsync(id);
                    if (se is not null)
                    {
                        context.Statistics.Remove(se);
                        return await context.SaveChangesAsync() == 1;
                    }
                }
                return false;
            }
        }
    }
}
