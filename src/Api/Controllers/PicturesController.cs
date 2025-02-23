using Api.Controllers.Base;
using Application.DTOs.Pictures;
using Application.UseCases.Pictures.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/pictures")]
    [ApiController]
    public class PicturesController : BaseController
    {
        private readonly IMediator _mediator;

        public PicturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Uploads an image for a specific product identified by the provided product ID.
        /// Validates the product ID, and if valid, attempts to add the image to the system.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to associate the image with.</param>
        /// <param name="request">The picture request containing the image data and additional information.</param>
        /// <returns>An IActionResult indicating the outcome of the image upload operation.</returns>
        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid productId, [FromForm] PictureRequest request)
        {
            if (productId == Guid.Empty)
                return BadRequest("Invalid request");

            var addPictureRequest = new AddPicture(request, productId);
            var result = await _mediator.Send(addPictureRequest);

            return SendResult(result);
        }


        //[HttpPost("{productId:guid}")]
        //public async Task<IActionResult> UploadImage([FromRoute] Guid productId, [FromForm] PictureRequest request)
        //{
        //    if (productId == Guid.Empty)
        //        return BadRequest("Invalid request");

        //    if (request.ImageData != null && request.ImageData.Length > 0)
        //    {
        //        // Since ImageData is already a byte array, no need to use CopyToAsync.
        //        var imageData = request.ImageData;  // Directly use the byte array

        //        var pictureRequest = new PictureRequest
        //        {
        //            Name = request.Name,
        //            ImageData = imageData
        //        };

        //        var addPictureRequest = new AddPicture(pictureRequest, productId);

        //        var result = await _mediator.Send(addPictureRequest);

        //        if (result.IsSuccess)
        //        {
        //            return Ok(result.Value);
        //        }

        //        return BadRequest();
        //    }

        //    return BadRequest("No image file uploaded.");
        //}

    }

}
