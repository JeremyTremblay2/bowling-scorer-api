using BowlingGrpcServer.Protos;
using BowlingGrpcServer.Utils;
using DTOtoModel;
using Grpc.Core;
using Model;
using Model.Players;
using Repositories;

namespace BowlingGrpcServer.Services
{
    public class StatisticService : StatisticGRPCService.StatisticGRPCServiceBase
    {
        private readonly ILogger<StatisticService> _logger;
        private readonly IStatisticRepository _statisticRepository;

        public StatisticService(ILogger<StatisticService> logger, IStatisticRepository statisticRepository)
        {
            _logger = logger;
            _statisticRepository = statisticRepository;
        }

        public override async Task<GetAllStatisticReply> GetAll(GetAllStatisticRequest request, ServerCallContext context)
        {
            IEnumerable<Statistics> statisticsFound = await _statisticRepository.GetAll(request.Page, request.NbStat);
            List<StatisticGRPC> statistics = new List<StatisticGRPC>();
            foreach (Statistics statistic in statisticsFound)
            {
                statistics.Add(statistic.ToGRPC());
            }
            GetAllStatisticReply getAllStatisticReply = new GetAllStatisticReply();
            getAllStatisticReply.StatisticGRPC.AddRange(statistics);
            return getAllStatisticReply;
        }

        public override async Task<GetByIdStatisticReply> GetById(GetByIdStatisticRequest request, ServerCallContext context)
        {
            GetByIdStatisticReply getByIdStatisticReply = new GetByIdStatisticReply();
            var result = await _statisticRepository.GetById(request.Id);
            if (result is not null)
            {
                getByIdStatisticReply.StatisticGRPC = result.ToGRPC();
            }
            return getByIdStatisticReply;
        }

        public override async Task<AddStatisticReply> AddStatistic(AddStatisticRequest request, ServerCallContext context)
        {
            AddStatisticReply addStatisticReply = new AddStatisticReply();
            var ok = await _statisticRepository.AddStatistics(new Statistics(request.Id, request.NumberOfVictory, request.NumberOfDefeat, request.NumberOfGames, request.BestScore));
            if (ok)
            {
                addStatisticReply.Response = "Added the statistic.";
            }
            else
            {
                addStatisticReply.Response = "Can't add the statistic (he already exists or id is not suitable)";
            }
            return addStatisticReply;
        }

        public override async Task<EditStatisticReply> EditStatistic(EditStatisticRequest request, ServerCallContext context)
        {
            EditStatisticReply editStatisticReply = new EditStatisticReply();
            var ok = await _statisticRepository.EditStatistics(new Statistics(request.Id, request.NumberOfVictory, request.NumberOfDefeat, request.NumberOfGames, request.BestScore));
            if (ok)
            {
                editStatisticReply.Response = "Edited the statistic.";
            }
            else
            {
                editStatisticReply.Response = "Can't edit the statistic (he don't exists)";
            }
            return editStatisticReply;
        }

        public override async Task<DeleteStatisticReply> DeleteStatistic(DeleteStatisticRequest request, ServerCallContext context)
        {
            DeleteStatisticReply deleteStatisticReply = new DeleteStatisticReply();
            var ok = await _statisticRepository.RemoveStatistics(request.Id);
            if (ok)
            {
                deleteStatisticReply.Response = "Deleted the statistic.";
            }
            else
            {
                deleteStatisticReply.Response = "Can't delete the statistic (he don't exists)";
            }
            return deleteStatisticReply;
        }
    }
}
