using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StatisticsAlreadyExistsException : ArgumentException
    {
        public StatisticsAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}
