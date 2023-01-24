using Entities;
using EntityToModel;
using Microsoft.EntityFrameworkCore;
using Model;
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
    public class PlayerRepository : IPlayerRepository
    {
        public async Task<IEnumerable<Player>> GetAll()
        {
            using (BowlingDbContext context = new())
            {
                List<Player> players = new();
                if (context.Players is not null)
                {
                    players = await context.Players
                        .Select(pl => pl.ToModel())
                        .ToListAsync(); // force the query
                }
                return players;
            }
        }

        public async Task<bool> AddPlayer(Player player)
        {
            using (BowlingDbContext context = new())
            {
                if (context.Players is not null)
                {
                    await context.Players.AddAsync(player.ToEntity());
                    return await context.SaveChangesAsync() == 1;
                }
                return false;
            }
        }

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
