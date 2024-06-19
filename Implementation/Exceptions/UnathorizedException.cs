using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Exceptions
{
    internal class UnathorizedException : Exception
    {
        public UnathorizedException(string message) : base(message)
        {

        }
    }
}
