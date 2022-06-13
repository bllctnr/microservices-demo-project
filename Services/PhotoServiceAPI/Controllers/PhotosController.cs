using Core.Constants;
using Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoService.Dto;

namespace PhotoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = "photos/" + photo.FileName;
                PhotoDto photoDto = new() { Url = returnPath };

                return Ok(new SuccessJsonDataResult<PhotoDto>(photoDto, Messages.RecordsAdded));
            }

            return BadRequest(new ErrorJsonResult(Messages.PhotoCouldNotSaved));
        }

        [HttpGet]
        public async Task<IActionResult> PhotoDelete(string photoUrl) 
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Ok(new SuccessJsonResult(Messages.PhotoDeleted));
            }
        
            return BadRequest(new ErrorJsonResult());
        }
    }
}
