using Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityToModel
{
    public class StatisticsExtensions
    {
        public Statistics ToModel(StatisticsEntity entity)
        {
            return new Statistics(entity.Player.ToModel(), entity.NumberOfVictory, entity.NumberOfDefeat, /*entity.Scores*/ null, entity.ID);
        }

        public StatisticsEntity ToEntity(Statistics model)
        {
            return new StatisticsEntity
            {
                ID = model.ID,
                NumberOfDefeat = model.NumberOfDefeat,
                NumberOfVictory = model.NumberOfVictory,
                NumberOfGames = model.NumberOfGames,
                //Scores = model.Scores,
                BestScore = model.BestScore
            };
        }
    }
}
