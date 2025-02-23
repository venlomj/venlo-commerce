using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Attributes.Interfaces;

namespace Domain.Settings
{
    public class Document : IDocument
    {
        public Guid Id { get; set; }
    }
}
