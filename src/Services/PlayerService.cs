using DTOs;
using Model;
using Model.Players;
using Repositories;
using System.Numerics;

namespace Services
{
    /// <summary>
    /// This class implements the interface that defines the contract for player service operations.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        /// <summary>
        /// The repository used by this service.
        /// </summary>
        private readonly IPlayerRepository _playerRepository;

        /// <summary>
        /// Create a new instance of PlayerService.
        /// </summary>
        /// <param name="playerRepository">The player repository.</param>
        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Gets a list of all players, paged by the specified number of players per page.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="nbPlayers">The number of players per page.</param>
        /// <returns>An enumerable list of players.</returns>
        public async Task<IEnumerable<Player>> GetAll(int page, int nbPlayers)
        {
            return await _playerRepository.GetAll(page, nbPlayers);
        }

        /// <summary>
        /// Gets a player by its id.
        /// </summary>
        /// <param name="id">The id of the player to retrieve.</param>
        /// <returns>The player with the specified id, or null if it does not exist.</returns>
        public async Task<Player> GetById(int id)
        {
            Player? player = await _playerRepository.GetById(id);
            if (player is null)
            {
                throw new FunctionnalException("This player doesn't exists.");
            }
            return player;
        }

        /// <summary>
        /// Adds a new player to the system.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>True if the player was added successfully, false otherwise.</returns>
        public async Task<bool> AddPlayer(Player player)
        {
            if (GetById(player.ID) is null)
            {
                throw new FunctionnalException("A player with the same ID already exists");
            }
            return await _playerRepository.AddPlayer(player);
        }

        /// <summary>
        /// Edits an existing player in the system.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <returns>True if the player was edited successfully, false otherwise.</returns>
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

        /// <summary>
        /// Deletes a player from the system.
        /// </summary>
        /// <param name="id">The id of the player to delete.</param>
        /// <returns>True if the player was deleted successfully, false otherwise.</returns>
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