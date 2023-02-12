using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// This Entity represents a Player in a database.
    /// </summary>
    public class PlayerEntity
    {
        /// <summary>
        /// The ID of the player.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The image of the player.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// The player's statistics.
        /// </summary>
        //public StatisticsEntity Statistics { get; set; }
    }
}