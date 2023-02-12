using DTOs;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOtoModel
{
    /// <summary>
    /// Extension class for Statistics to convert between Statistics Model and StatisticDTO.
    /// </summary>
    public static class StatisticsExtension
    {
        /// <summary>
        /// Converts a Statistics object to a StatisticDTO object.
        /// </summary>
        /// <param name="statistics">Statistics object to be converted</param>
        /// <returns>A StatisticDTO object</returns>
        public static StatisticDTO ToDTO(this Statistics statistics)
        {
            return new StatisticDTO
            {
                ID = statistics.ID,
                NumberOfDefeat = statistics.NumberOfDefeat,
                NumberOfVictory = statistics.NumberOfVictory,
                NumberOfGames = statistics.NumberOfGames,
                BestScore = statistics.BestScore
            };
        }

        /// <summary>
        /// Converts a StatisticDTO object to a Statistics object.
        /// </summary>
        /// <param name="statisticsDTO">StatisticDTO object to be converted</param>
        /// <returns>A Statistics object</returns>
        public static Statistics ToModel(this StatisticDTO statisticsDTO)
        {
            return new Statistics(statisticsDTO.ID, statisticsDTO.NumberOfVictory,
                statisticsDTO.NumberOfDefeat, statisticsDTO.NumberOfGames, statisticsDTO.BestScore);
        }
    }

}
