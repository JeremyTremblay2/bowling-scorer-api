using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Interface used by DataManagers, allow the developer to choose the persistance mode by using
    /// polymorphism (strategy). Contains methods to add, ge tand remove players and games.
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Return all the players in the DB
        /// </summary>
        /// <returns>All players in the DB</returns>
        Task<IEnumerable<Player>> GetAll();

        /// <summary>
        /// Get the first players specified from the stated index ordered by the ID.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <returns>The collection of players retrieve.</returns>
        Task<IEnumerable<Player>> GetPlayers(int index, int count);

        /// <summary>
        /// Return the speficied player if he exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player if he exists, null if he doesn't exists</returns>
        Task<Player?> GetById(int id);

        /// <summary>
        /// Add a player to the DB.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        Task<bool> AddPlayer(Player player);

        /// <summary>
        /// Change the name and the image of the specified player.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <returns>A boolean indicating if the player was succesfully updated.</returns>
        Task<bool> EditPlayer(Player player);

        /// <summary>
        /// Remove a player from the manager.
        /// </summary>
        /// <param name="id">Id of the player to remove</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        Task<bool> RemovePlayer(int id);
    }
}
