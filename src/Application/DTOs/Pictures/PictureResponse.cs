using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pictures
{
    public class PictureResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public required byte[] ImageData { get; set; }

        public Guid ImageIdentifier { get; set; }

        public Guid ProductId { get; set; }
        public string ImageType { get; set; }
    }
}
