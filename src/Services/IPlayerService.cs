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
        Task<IEnumerable<Player>> GetAll();
        Task<Player?> GetById(int id);
        void AddPlayer(Player player);
        void EditPlayer(Player player);
        void DeletePlayer(int id);
    }
}
