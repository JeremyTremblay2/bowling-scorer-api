using Services;

namespace GrpcAPI.Services
{
    public class PlayerGRPCService : PlayerProtoService.PlayerProtoServiceBase
    {
        private readonly ILogger<PlayerGRPCService> _logger;
        private readonly IPlayerService _playerService;

        public GreeterService(ILogger<PlayerGRPCService> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        public override Task<PlayerReply> GetPlayerById(PlayerRequest playerRequest, ServerCallContext serverCallContext)
        {
            return Task.FromResult(_playerService.GetById(playerRequest.player_id));
        }
    }
}
