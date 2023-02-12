using DTOs;
using Model;
using Model.Players;

namespace DTOtoModel
{
    public static class PlayerExtention
    {
        public static PlayerDTO ToDTO(this Player player)
        {
            return new PlayerDTO
            {
                ID = player.ID,
                Image = player.Image,
                Name = player.Name
            };
        }

        public static Player ToModel(this PlayerDTO playerDTO)
        {
            return new Player(playerDTO.ID, playerDTO.Name, playerDTO.Image);
        }
    }
}