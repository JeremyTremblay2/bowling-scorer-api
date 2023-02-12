using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Represents an error when a Statistics object was not found.
    /// </summary>
    public class StatisticsNotFoundException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public StatisticsNotFoundException(string? message) : base(message)
        {
        }
    }
}
