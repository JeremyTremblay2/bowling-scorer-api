using DTOs;
using Model;
using Model.Players;

namespace DTOtoModel
{
    /// <summary>
    /// The PlayerExtension class contains methods to convert between the Player model and PlayerDTO data transfer objects.
    /// </summary>
    public static class PlayerExtension
    {
        /// <summary>
        /// Converts a Player object to a PlayerDTO object.
        /// </summary>
        /// <param name="player">The Player object to be converted.</param>
        /// <returns>A PlayerDTO object.</returns>
        public static PlayerDTO ToDTO(this Player player)
        {
            return new PlayerDTO
            {
                ID = player.ID,
                Image = player.Image,
                Name = player.Name
            };
        }

        /// <summary>
        /// Converts a PlayerDTO object to a Player object.
        /// </summary>
        /// <param name="playerDTO">The PlayerDTO object to be converted.</param>
        /// <returns>A Player object.</returns>
        public static Player ToModel(this PlayerDTO playerDTO)
        {
            return new Player(playerDTO.ID, playerDTO.Name, playerDTO.Image);
        }
    }
}