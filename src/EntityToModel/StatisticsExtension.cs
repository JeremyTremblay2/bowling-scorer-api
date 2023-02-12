using Entities;
using Model;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityToModel
{
    /// <summary>
    /// Extension class to convert between the Statistics model and its entity representation.
    /// </summary>
    public static class StatisticsExtension
    {
        /// <summary>
        /// Converts a Statistics model to a StatisticsEntity.
        /// </summary>
        /// <param name="statistics">The Statistics model to be converted.</param>
        /// <returns>A StatisticsEntity that represents the Statistics model.</returns>
        public static StatisticsEntity ToEntity(this Statistics statistics)
        {
            return new StatisticsEntity
            {
                ID = statistics.ID,
                NumberOfVictory = statistics.NumberOfVictory,
                NumberOfGames = statistics.NumberOfGames,
                BestScore = statistics.BestScore,
                NumberOfDefeat = statistics.NumberOfDefeat
            };
        }

        /// <summary>
        /// Converts a StatisticsEntity to a Statistics model.
        /// </summary>
        /// <param name="statisticsEntity">The StatisticsEntity to be converted.</param>
        /// <returns>A Statistics model that represents the StatisticsEntity.</returns>
        public static Statistics ToModel(this StatisticsEntity statisticsEntity)
        {
            return new Statistics(statisticsEntity.ID, statisticsEntity.NumberOfVictory, statisticsEntity.NumberOfDefeat, statisticsEntity.NumberOfGames, statisticsEntity.BestScore);
        }
    }

}
