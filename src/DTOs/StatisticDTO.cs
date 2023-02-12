using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DTOs
{
    /// <summary>
    /// Represents a data transfer object for Statistics.
    /// </summary>
    public class StatisticDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the statistic.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the number of victories.
        /// </summary>
        public int NumberOfVictory { get; set; }
        /// <summary>
        /// Gets or sets the number of defeats.
        /// </summary>
        public int NumberOfDefeat { get; set; }
        /// <summary>
        /// Gets or sets the total number of games.
        /// </summary>
        public int NumberOfGames { get; set; }
        /// <summary>
        /// Gets or sets the best score.
        /// </summary>
        public int BestScore { get; set; }

        /// <summary>
        /// Returns a string representation of the StatisticDTO.
        /// </summary>
        /// <returns>A string representation of the StatisticDTO.</returns>
        public override string ToString()
        {
            return $"StatisticDTO[ID:{ID}, NumberOfVictory:{NumberOfVictory}, NumberOfDefeat:{NumberOfDefeat}, NumberOfGames:{NumberOfGames}, BestScore:{BestScore}]";
        }
    }
}
