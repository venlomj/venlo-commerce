using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public string Description { get; } = string.Empty;
        public IEnumerable<string> Parameters { get; } = Enumerable.Empty<string>();
        public BusinessLogicException(string message) : base(message) { }

        public BusinessLogicException(string message, string description) : base(message)
        {
            Description = description;
        }

        public BusinessLogicException(string message, string description, IEnumerable<string> parameters) : base(description)
        {
            Description = description;
            Parameters = parameters;
        }
    }
}
