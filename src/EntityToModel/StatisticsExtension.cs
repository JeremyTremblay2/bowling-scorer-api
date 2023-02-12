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
    public static class StatisticsExtension
    {
        public static StatisticEntity ToEntity(this Statistics statistics)
        {
            return new StatisticEntity
            {
                ID = statistics.ID,
                NumberOfVictory= statistics.NumberOfVictory,
                NumberOfGames= statistics.NumberOfGames,
                BestScore= statistics.BestScore,
                NumberOfDefeat = statistics.NumberOfDefeat
            };
        }

        public static Statistics ToModel(this StatisticEntity statisticsEntity)
        {
            return new Statistics(statisticsEntity.ID, statisticsEntity.NumberOfVictory, statisticsEntity.NumberOfDefeat, statisticsEntity.NumberOfGames, statisticsEntity.BestScore);
        }
    }
}
