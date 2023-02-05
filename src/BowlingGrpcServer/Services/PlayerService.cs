using BowlingGrpcServer;
using DTOtoModel;
using Grpc.Core;
using Model;
using Repositories;

namespace BowlingGrpcServer.Services
{
    public class PlayerService : PlayerGRPCService.PlayerGRPCServiceBase
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(ILogger<PlayerService> logger, IPlayerRepository playerRepository)
        {
            _logger = logger;
            _playerRepository = playerRepository;
        }

        public override async Task<GetAllReply> GetAll(GetAllRequest request, ServerCallContext context)
        {
            IEnumerable<Player> playersFound = await _playerRepository.GetAll(request.Page, request.NbPlayers);
            List<PlayerGRPC> players = new List<PlayerGRPC>();
            foreach (Player player in playersFound)
            {
                players.Add(player.ToGRPC());
            }
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

        public override async Task<AddPlayerReply> AddPlayer(AddPlayerRequest request, ServerCallContext context)
        {
            AddPlayerReply addPlayerReply = new AddPlayerReply();
            addPlayerReply.Response = "Added the player.";
            return addPlayerReply;
        }

        public override async Task<EditPlayerReply> EditPlayer(EditPlayerRequest request, ServerCallContext context)
        {
            EditPlayerReply editPlayerReply = new EditPlayerReply();
            editPlayerReply.Response = "Edited the player.";
            return editPlayerReply;
        }

        public override async Task<DeletePlayerReply> DeletePlayer(DeletePlayerRequest request, ServerCallContext context)
        {
            DeletePlayerReply deletePlayerReply = new DeletePlayerReply();
            deletePlayerReply.Response = "Deleted the player. ";
            return deletePlayerReply;
        }
    }
}