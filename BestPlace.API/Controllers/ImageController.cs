using BestPlace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private  readonly  IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public async Task<IActionResult>  GetCategoryImage(Guid id)
        {
            var binary = await this.imageService.GetCategoryImage(id);

            return File(binary, "image/png");
        }

    }
}