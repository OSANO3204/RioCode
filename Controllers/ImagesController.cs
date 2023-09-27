using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RioUpesi.INFRASTRUCTURE.Iservices.IImagesServices;

namespace RioUpesi.Controllers
{
    public class ImagesController
    {
        private readonly IImagesServices _imagesService;
        public ImagesController(IImagesServices imagesService)
        {
            _imagesService= imagesService;
        }

        [Authorize]
        [Route("AddRoomPicture")]
        [HttpPost]
        public async Task<string> Add_Room_Picture(IFormFile file, string Image_Description)
        {
              if(file == null)
                {
                    return "Kindly choose a file ";
                }
        var res= await _imagesService.Add_Room_Picture(file, Image_Description);
         return res;
        }
    }
}

