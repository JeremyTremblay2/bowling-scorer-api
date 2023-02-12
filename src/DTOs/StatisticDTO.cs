using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DTOs
{
    public class StatisticDTO
    {
        public int ID { get; set; }
        public int NumberOfVictory { get; set; }
        public int NumberOfDefeat { get; set; }
        public int NumberOfGames { get; set; }
        public int BestScore { get; set; }

        public override string ToString()
        {
            return $"StatisticDTO[ID:{ID}, NumberOfVictory:{NumberOfVictory}, NumberOfDefeat:{NumberOfDefeat}, NumberOfGames:{NumberOfGames}, BestScore:{BestScore}]";
        }
    }
}
