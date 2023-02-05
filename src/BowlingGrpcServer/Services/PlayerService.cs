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
            var result = await _playerRepository.GetById(request.Id);
            if (result is not null)
            {
                getByIdReply.PlayerGRPC = result.ToGRPC();
            }
            return getByIdReply;
        }

        public override async Task<AddPlayerReply> AddPlayer(AddPlayerRequest request, ServerCallContext context)
        {
            AddPlayerReply addPlayerReply = new AddPlayerReply();
            var ok = await _playerRepository.AddPlayer(new Player(request.Id, request.Name, request.Image));
            if (ok)
            {
                addPlayerReply.Response = "Added the player.";
            }
            else
            {
                addPlayerReply.Response = "Can't add the player (he already exists or id is not suitable)";
            }
            return addPlayerReply;
        }

        public override async Task<EditPlayerReply> EditPlayer(EditPlayerRequest request, ServerCallContext context)
        {
            EditPlayerReply editPlayerReply = new EditPlayerReply();
            var ok = await _playerRepository.EditPlayer(new Player(request.Id, request.Name, request.Image));
            if (ok)
            {
                editPlayerReply.Response = "Edited the player.";
            }
            else
            {
                editPlayerReply.Response = "Can't edit the player (he don't exists)";
            }
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