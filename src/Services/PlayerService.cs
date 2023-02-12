using DTOs;
using Model;
using Model.Players;
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

        public async Task<IEnumerable<Player>> GetAll(int page, int nbPlayers)
        {
            return await _playerRepository.GetAll(page, nbPlayers);
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
             

        public async Task<bool> AddPlayer(Player player)
        {
            if (GetById(player.ID) is null)
            {
                throw new FunctionnalException("A player with the same ID already exists");
            }
            return await _playerRepository.AddPlayer(player);
        }

        public async Task<bool> EditPlayer(Player player)
        {
            Player? p = await GetById(player.ID);
            if (p is null)
            {
                throw new FunctionnalException("The player that you want to edit doesn't exists");
            }
            else if (p.Equals(player))
            {
                return true;
            }
            else
            {
                return await _playerRepository.EditPlayer(player);
            }
        }

        public async Task<bool> DeletePlayer(int id)
        {
            if (GetById(id) is null)
            {
                throw new FunctionnalException("The player that you want to delete doesn't exists");
            }
            return await _playerRepository.RemovePlayer(id);
        }
    }
}