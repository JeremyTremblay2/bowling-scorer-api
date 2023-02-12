using Entities;
using Model;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityToModel
{
    /// <summary>
    /// Extension class for Player model.
    /// </summary>
    public static class PlayerExtension
    {
        /// <summary>
        /// Extension method that converts a Player object to a PlayerEntity object.
        /// </summary>
        /// <param name="player">The Player object to convert.</param>
        /// <returns>A PlayerEntity object representing the Player object.</returns>
        public static PlayerEntity ToEntity(this Player player)
        {
            return new PlayerEntity
            {
                ID = player.ID,
                Image = player.Image,
                Name = player.Name,
                //Statistics = player.Statistics.ToEntity()
            };
        }

        /// <summary>
        /// Extension method that converts a PlayerEntity object to a Player object.
        /// </summary>
        /// <param name="playerEntity">The PlayerEntity object to convert.</param>
        /// <returns>A Player object representing the PlayerEntity object.</returns>
        public static Player ToModel(this PlayerEntity playerEntity)
        {
            return new Player(playerEntity.ID, playerEntity.Name, playerEntity.Image /*playerEntity.Statistics.ToModel()*/);
        }
    }

}
