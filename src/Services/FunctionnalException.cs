using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    /// <summary>
    /// A custom exception class that represents a functional error.
    /// </summary>
    public class FunctionnalException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionnalException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public FunctionnalException(string? message) : base(message)
        {
        }
    }
}
