using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class FormFileToByteArrayConverter : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
        {
            if (source == null) return null;

            using var memoryStream = new MemoryStream();
            source.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
