using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Represents an error when a Statistics object is not valid.
    /// </summary>
    public class StatisticsNotValidException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsNotValidException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public StatisticsNotValidException(string message) : base(message)
        {

        }
    }
}
