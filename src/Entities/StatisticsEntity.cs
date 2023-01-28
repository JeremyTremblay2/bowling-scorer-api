using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// This Entity represents some statistics in a database.
    /// </summary>
    [Table("Statistics")]
    public class StatisticsEntity
    {
        /// <summary>
        /// Represents the ID of the statistics.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The player who owns these statistics.
        /// </summary>
        public PlayerEntity Player { get; set; }

        /// <summary>
        /// Represents the number of total wins by a player.
        /// </summary>
        public int NumberOfVictory { get; set; }

        /// <summary>
        /// Represents the total number of games lost by a player.
        /// </summary>
        public int NumberOfDefeat { get; set; }

        /// <summary>
        /// Represents the total number of games a player has participated in.
        /// </summary>
        public int NumberOfGames { get; set; }

        /// <summary>
        /// Represents the player's best score.
        /// </summary>
        public int BestScore { get; set; }

        /// <summary>
        /// Contains all the scores achieved by the player in each game played.
        /// </summary>
        //public ICollection<int> Scores { get; set; }
    }
}
