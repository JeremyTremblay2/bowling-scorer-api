using Entities;
using EntityToModel;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Players;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Class implementing the interface used by DataManagers, allow the developer to choose the persistance mode by using
    /// polymorphism (strategy). Contains methods to add, ge tand remove players and games.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        /// <summary>
        /// Return all the players in the DB
        /// </summary>
        /// <returns>All players in the DB</returns>
        public async Task<IEnumerable<Player>> GetAll(int page, int nbPlayers)
        {
            using (BowlingDbContext context = new())
            {
                List<Player> players = new();
                if (context.Players is not null)
                {
                    players = await context.Players
                        .Skip(nbPlayers*page)
                        .Take(nbPlayers)
                        .Select(pl => pl.ToModel())
                        .ToListAsync(); // force the query
                }
                return players;
            }
        }

        /// <summary>
        /// Add a player to the DB.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public async Task<bool> AddPlayer(Player player)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Players is not null)
                {
                    await context.Players.AddAsync(player.ToEntity());
                    try
                    {
                        return await context.SaveChangesAsync() == 1;
                    }
                    catch (DbUpdateException e)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Return the speficied player if he exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player if he exists, null if he doesn't exists</returns>
        public async Task<Player?> GetById(int id)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Players is not null)
                {
                    PlayerEntity? pe = await context.Players.FindAsync(id);
                    if (pe is not null)
                    {
                        return pe.ToModel();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Change the name and the image of the specified player.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <returns>A boolean indicating if the player was succesfully updated.</returns>
        public async Task<bool> EditPlayer(Player player)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Players is not null)
                {
                    PlayerEntity? pe = await context.Players.FindAsync(player.ID);
                    if (pe != null)
                    {
                        pe.Name = player.Name;
                        pe.Image = player.Image;
                        return await context.SaveChangesAsync() == 1;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get the first players specified from the stated index ordered by the ID.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <returns>The collection of players retrieve.</returns>
        public async Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            using (BowlingDbContext context = new())
            {
                List<Player> players = new();
                if (context.Players is not null)
                {
                    players = await context.Players.Skip(index * count)
                        .Take(count)
                        .Select(pl => pl.ToModel())
                        .ToListAsync();
                }
                return players;
            }
        }

        /// <summary>
        /// Remove a player from the manager.
        /// </summary>
        /// <param name="id">Id of the player to remove</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        public async Task<bool> RemovePlayer(int id)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Players is not null)
                {
                    PlayerEntity? pe = await context.Players.FindAsync(id);
                    if (pe is not null)
                    {
                        context.Players.Remove(pe);
                        return await context.SaveChangesAsync() == 1;
                    }
                }
                return false;
            }
        }
    }
}
