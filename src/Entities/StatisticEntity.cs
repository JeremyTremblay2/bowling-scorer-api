using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatisticEntity
    {
        public int ID { get; set; }
        public int NumberOfVictory { get; set; }
        public int NumberOfDefeat { get; set; }
        public int NumberOfGames { get; set; }
        public int BestScore { get; set; }
    }
}
