using BowlingGrpcServer.Protos;
using Model;
using Model.Players;

namespace BowlingGrpcServer.Utils
{
    public static class StatisticExtension
    {
        public static StatisticGRPC ToGRPC(this Statistics statistics)
        {
            return new StatisticGRPC
            {
                Id = statistics.ID,
                BestScore= statistics.BestScore,
                NumberOfDefeat= statistics.NumberOfDefeat,
                NumberOfGames= statistics.NumberOfGames,
                NumberOfVictory = statistics.NumberOfVictory
            };
        }

        public static Statistics ToModel(this StatisticGRPC statisticGRPC)
        {
            return new Statistics(statisticGRPC.Id, statisticGRPC.NumberOfVictory, statisticGRPC.NumberOfDefeat, statisticGRPC.NumberOfGames, statisticGRPC.BestScore);
        }
    }
}
