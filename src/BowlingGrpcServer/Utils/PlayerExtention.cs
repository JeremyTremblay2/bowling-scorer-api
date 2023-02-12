using BowlingGrpcServer;
using Model;
using Model.Players;

namespace DTOtoModel
{
    public static class PlayerExtention
    {
        public static PlayerGRPC ToGRPC(this Player player)
        {
            return new PlayerGRPC
            {
                Id = player.ID,
                Image = player.Image,
                Name = player.Name
            };
        }

        public static Player ToModel(this PlayerGRPC playerGRPC)
        {
            return new Player(playerGRPC.Id, playerGRPC.Name, playerGRPC.Image);
        }
    }
}