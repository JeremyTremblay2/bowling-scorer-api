using DTOs;
using Model;
using Repositories;
using System.Numerics;

namespace Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await _playerRepository.GetAll();
        }

        public async Task<Player> GetById(int id)
        {
            Player? player = await _playerRepository.GetById(id);
            if (player is null)
            {
                throw new FunctionnalException("This player doesn't exists.");
            }
            return player;
        }
             

        public async Task AddPlayer(Player player)
        {
            if (GetById(player.ID) is not null)
            {
                throw new FunctionnalException("A player with the same ID already exists");
            }
            if (!await _playerRepository.AddPlayer(player))
            {
                throw new FunctionnalException("Failed to add the player (error while saving)");
            }
        }

        public async Task EditPlayer(Player player)
        {
            if (GetById(player.ID) is null || !await _playerRepository.EditPlayer(player))
            {
                throw new FunctionnalException("The player that you want to edit doesn't exists");
            }
        }

        public async Task DeletePlayer(int id)
        {
            if (GetById(id) is null || !await _playerRepository.RemovePlayer(id))
            {
                throw new FunctionnalException("The player that you want to delete doesn't exists");
            }
        }
    }
}