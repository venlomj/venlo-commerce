using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Attributes.Interfaces
{
    public interface IDocument
    {
        Guid Id { get; set; }
    }
}
