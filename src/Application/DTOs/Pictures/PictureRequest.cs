using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Pictures
{
    public class PictureRequest
    {
        public string Name { get; set; }
        public IFormFile ImageData { get; set; }
    }
}
