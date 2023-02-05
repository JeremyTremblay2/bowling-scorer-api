using BowlingGrpcServer;
using Grpc.Core;

namespace BowlingGrpcServer.Services
{
    public class PlayerService : Player.PlayerBase
    {
        private readonly ILogger<PlayerService> _logger;
        public PlayerService(ILogger<PlayerService> logger)
        {
            _logger = logger;
        }

        public override async Task<GetAllReply> GetAll(GetAllRequest request, ServerCallContext context)
        {
            List<PlayerGRPC> players = new List<PlayerGRPC>()
            {
                new PlayerGRPC(){ Id = 1, Image = "truc", Name = "machin.png"},
                new PlayerGRPC(){ Id = 1, Image = "truc", Name = "machin.png"},
                new PlayerGRPC(){ Id = 1, Image = "truc", Name = "machin.png"},
            };
            GetAllReply getAllReply = new GetAllReply();
            getAllReply.PlayerGRPC.AddRange(players);
            return getAllReply;
        }

        public override async Task<GetByIdReply> GetById(GetByIdRequest request, ServerCallContext context)
        {
            GetByIdReply getByIdReply = new GetByIdReply();
            getByIdReply.PlayerGRPC = new PlayerGRPC() { Id = 1, Image = "truc", Name = "machin.png" };
            return getByIdReply;
        }
    }
}