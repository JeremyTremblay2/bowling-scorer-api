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

        public async Task<Player?> GetById(int id)
        {
            return await _playerRepository.GetById(id);
        }

        public async void AddPlayer(Player player)
        {
            if (GetById(player.ID) is null)
            {
                throw new FunctionnalException("A player with the same ID already exists");
            }
            await _playerRepository.AddPlayer(player);
        }

        public async void EditPlayer(Player player)
        {
            if (GetById(player.ID) is null)
            {
                throw new FunctionnalException("The player that you want to edit doesn't exists");
            }
            await _playerRepository.EditPlayer(player);
        }

        public async void DeletePlayer(int id)
        {
            if (GetById(id) is null)
            {
                throw new FunctionnalException("The player that you want to delete doesn't exists");
            }
            await _playerRepository.RemovePlayer(id);
        }
    }
}