using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StatisticsNotValidException : ArgumentException
    {
        public StatisticsNotValidException(string message) : base(message)
        {

        }
    }
}
