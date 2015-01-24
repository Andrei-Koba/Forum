using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Implementation
{
    public class RepositoryExceptions : Exception
    {
        public RepositoryExceptions(string message) : base(message) { }

        public RepositoryExceptions(string message, Exception exception)
            : base(message, exception)
        { }
    }
}
