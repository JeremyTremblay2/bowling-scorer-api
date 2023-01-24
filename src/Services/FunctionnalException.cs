using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class FunctionnalException : ArgumentException
    {
        public FunctionnalException(string? message) : base(message)
        {
        }
    }
}
