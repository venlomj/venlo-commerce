using Application.DTOs.Products;
using Application.UseCases.Products.Commands;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Pictures;
using AutoMapper;
using Domain.Documents;
using Domain.Repositories.Pictures;
using Domain.UoW;
using Domain.Repositories.Products;
using Domain.Exceptions;

namespace Application.UseCases.Pictures.Command.Handler
{
    public class AddPictureHandler : IRequestHandler<AddPicture, Result<PictureResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<ProductImage> _imageRepository;
        private readonly IProductsReaderRepository _productsReader;

        public AddPictureHandler(IMapper mapper, IMongoRepository<ProductImage> imageRepository, IProductsReaderRepository productsReader)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _productsReader = productsReader;
        }

        public async Task<Result<PictureResponse>> Handle(AddPicture request, CancellationToken cancellationToken)
        {
            // Check if Product exists in SQL Database
            var productExists = await _productsReader.Exists(request.ProductId);
            if (!productExists)
            {
                return new BusinessLogicException($"Product with {request.ProductId} not found.");
            }

            // Convert the IFormFile (ImageData) to byte[] 
            byte[] imageData = null;
            if (request.PictureRequest.ImageData != null && request.PictureRequest.ImageData.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Copy the file content into the memory stream
                    await request.PictureRequest.ImageData.CopyToAsync(memoryStream, cancellationToken);

                    // Convert the memory stream to byte array
                    imageData = memoryStream.ToArray();
                }
            }

            // Create ProductImage object and assign byte array to ImageData
            var image = new ProductImage
            {
                Name = request.PictureRequest.Name,
                ImageData = imageData,
                ImageIdentifier = Guid.NewGuid(),
                ProductId = request.ProductId
            };

            // Insert image into the repository
            await _imageRepository.InsertOneAsync(image);

            // Retrieve the inserted image by its ID
            var newImage = await _imageRepository.FindByIdAsync(image.Id);

            // Map the retrieved image to a PictureResponse and return it
            return _mapper.Map<PictureResponse>(newImage);
        }

    }
}
