using DTOs;
using Model;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// This interface defines the contract for player service operations.
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Gets a list of all players, paged by the specified number of players per page.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="nbPlayers">The number of players per page.</param>
        /// <returns>An enumerable list of players.</returns>
        Task<IEnumerable<Player>> GetAll(int page, int nbPlayers);

        /// <summary>
        /// Gets a player by its id.
        /// </summary>
        /// <param name="id">The id of the player to retrieve.</param>
        /// <returns>The player with the specified id, or null if it does not exist.</returns>
        Task<Player> GetById(int id);

        /// <summary>
        /// Adds a new player to the system.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>True if the player was added successfully, false otherwise.</returns>
        Task<bool> AddPlayer(Player player);

        /// <summary>
        /// Edits an existing player in the system.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <returns>True if the player was edited successfully, false otherwise.</returns>
        Task<bool> EditPlayer(Player player);

        /// <summary>
        /// Deletes a player from the system.
        /// </summary>
        /// <param name="id">The id of the player to delete.</param>
        /// <returns>True if the player was deleted successfully, false otherwise.</returns>
        Task<bool> DeletePlayer(int id);
    }

}
