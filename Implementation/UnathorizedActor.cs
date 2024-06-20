using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class UnathorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string Username => "Unauthorized";

        public string Email => "Unauthorized";

        public string FirstName => "Unauthorized";

        public string LastName => "Unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1, 21, 22, 31, 32 };
    }
}
