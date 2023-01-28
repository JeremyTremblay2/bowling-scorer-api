using Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityToModel
{
    public static class PlayerExtension
    {
        public static PlayerEntity ToEntity(this Player player)
        {
            return new PlayerEntity
            {
                ID = player.ID,
                Image = player.Image,
                Name = player.Name,
                Statistics = player.Statistics.ToEntity()
            };
        }

        public static Player ToModel(this PlayerEntity playerEntity)
        {
            return new Player(playerEntity.ID, playerEntity.Name, playerEntity.Image, playerEntity.Statistics.ToModel());
        }
    }
}
