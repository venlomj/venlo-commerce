using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Pictures;

namespace Application.Services
{
    public interface IImageService
    {
        Task<Guid> UploadImage(Guid productId, PictureRequest request);
    }
}
