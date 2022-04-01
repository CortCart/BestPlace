using BestPlace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        { 
          this.imageService = imageService;
        }

        [HttpGet("GetCategoryImage/{id}")]
        public async Task<IActionResult> GetCategoryImage(Guid id)
        {
            try
            {
                var binary = await this.imageService.GetCategoryImage(id);

                return File(binary, "image/png");
            }
            catch
            {
                return BadRequest();
            }

           
        }

        [HttpGet("GetItemImage/{id}")]
        public async Task<IActionResult> GetItemImage(Guid id)
        {
            try
            {
                var binary = await this.imageService.GetItemImage(id);

                return File(binary, "image/png");
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}