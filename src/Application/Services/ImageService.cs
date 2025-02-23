using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Pictures;
using Domain.Documents;
using Domain.Repositories.Pictures;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IMongoRepository<ProductImage> _imageRepository;

        public ImageService(IMongoRepository<ProductImage> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Guid> UploadImage(Guid productId, PictureRequest request)
        {
            byte[] imageData;
            using var memoryStream = new MemoryStream();

            await request.ImageData.CopyToAsync(memoryStream);
            imageData = memoryStream.ToArray();

            var newImage = new ProductImage
            {
                ProductId = productId,
                ImageIdentifier = Guid.NewGuid(),
                ImageData = imageData
            };

            await _imageRepository.InsertOneAsync(newImage);

            return newImage.ImageIdentifier;
        }

    }
}
