namespace DTOs
{
    public class PlayerDTO : IEquatable<PlayerDTO>
    {
        /// <summary>
        /// A unique identifier for the player.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The profil picture of the player.
        /// </summary>
        public string Image { get; set; }

        public bool Equals(PlayerDTO? other)
        {
            if (other is null) return false;
            return other.ID == ID && other.Name== Name && other.Image == Image;
        }

        public override string ToString()
        {
            return $"PlayerDTO[ID:{ID}, Name:{Name}, Image:{Image}]";
        }
    }
}