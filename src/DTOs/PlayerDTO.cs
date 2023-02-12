namespace DTOs
{
    /// <summary>
    /// Represent a Data Transfert Object that contains the Player.
    /// </summary>
    public class PlayerDTO
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

        /// <summary>
        /// Display the object in a string version of it.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"PlayerDTO[ID:{ID}, Name:{Name}, Image:{Image}]";
        }
    }
}