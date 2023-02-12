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
        public int ID { get; private set; }
        public int NumberOfVictory { get; private set; }
        public int NumberOfDefeat { get; private set; }
        public int NumberOfGames { get; private set; }
        public int BestScore { get; private set; }

        public override string ToString()
        {
            return $"StatisticDTO[ID:{ID}, NumberOfVictory:{NumberOfVictory}, NumberOfDefeat:{NumberOfDefeat}, NumberOfGames:{NumberOfGames}, BestScore:{BestScore}]";
        }
    }
}
