using DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAll(int page, int nbPlayers);
        Task<Player> GetById(int id);
        Task<bool> AddPlayer(Player player);
        Task<bool> EditPlayer(Player player);
        Task<bool> DeletePlayer(int id);
    }
}
