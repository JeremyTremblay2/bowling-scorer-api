using Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityToModel
{
    public static class StatisticsExtensions
    {
        public static Statistics ToModel(this StatisticsEntity entity)
        {
            return new Statistics(/*entity.Player.ToModel(),*/ entity.NumberOfVictory, entity.NumberOfDefeat, /*entity.Scores*/ null, entity.ID);
        }

        public static StatisticsEntity ToEntity(this Statistics model)
        {
            return new StatisticsEntity
            {
                ID = model.ID,
                NumberOfDefeat = model.NumberOfDefeat,
                NumberOfVictory = model.NumberOfVictory,
                NumberOfGames = model.NumberOfGames,
                //Scores = model.Scores,
                BestScore = model.BestScore,
                //Player = model.Player.ToEntity()
            };
        }
    }
}
