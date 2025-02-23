using Application.Abstractions.Messaging;
using Application.DTOs.Products;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Pictures;

namespace Application.UseCases.Pictures.Command
{
    public class AddPicture : ICommand<Result<PictureResponse>>
    {
        public AddPicture(PictureRequest pictureRequest, Guid productId)
        {
            PictureRequest = pictureRequest;
            ProductId = productId;
        }

        public PictureRequest PictureRequest { get; set; }
        public Guid ProductId { get; set; }
    }
}
