using DTOs;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOtoModel
{
    public static class StatisticsExtension
    {
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

        public static Statistics ToModel(this StatisticDTO statisticsDTO)
        {
            return new Statistics(statisticsDTO.ID, statisticsDTO.NumberOfVictory, 
                statisticsDTO.NumberOfDefeat, statisticsDTO.NumberOfGames, statisticsDTO.BestScore);
        }
    }
}
