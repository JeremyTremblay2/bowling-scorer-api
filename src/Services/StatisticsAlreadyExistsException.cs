using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Exception class representing a statistics already exists error.
    /// </summary>
    public class StatisticsAlreadyExistsException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public StatisticsAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}
