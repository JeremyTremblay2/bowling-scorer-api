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

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello uwu " + request.Name
            });
        }
    }
}