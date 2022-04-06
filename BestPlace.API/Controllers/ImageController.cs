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
        public async Task<IActionResult> GetCategoryImage(string id)
        {
            try
            {
                var binary = await this.imageService.GetCategoryImage(Guid.Parse(id));

                return File(binary, "image/png");
            }
            catch
            {
                return BadRequest();
            }


        }

        [HttpGet("GetItemImage/{id}")]
        public async Task<IActionResult> GetItemImage(string id)
        {
            try
            {

                var binary = await this.imageService.GetItemImage(Guid.Parse(id));

                return File(binary, "image/png");
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("DeleteItemImage/{id}")]
        public async Task<IActionResult> DeleteImage(string id)
        {
            try
            {

                    await this.imageService.DeleteItemImage(Guid.Parse(id));
                return Ok();

            }
            catch
            {
                return BadRequest();
            }

        }
    }
}