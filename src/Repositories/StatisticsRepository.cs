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
    public class StatisticsRepository : IStatisticRepository
    {
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

        public async Task<bool> EditStatistics(Statistics statistics)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Statistics is not null)
                {
                    StatisticEntity? se = await context.Statistics.FindAsync(statistics.ID);
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

        public async Task<Statistics?> GetById(int id)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Statistics is not null)
                {
                    StatisticEntity? se = await context.Statistics.FindAsync(id);
                    if (se is not null)
                    {
                        return se.ToModel();
                    }
                }
                return null;
            }
        }

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

        public async Task<bool> RemoveStatistics(int id)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Statistics is not null)
                {
                    StatisticEntity? se = await context.Statistics.FindAsync(id);
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
