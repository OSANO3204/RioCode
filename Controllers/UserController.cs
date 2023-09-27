
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RioUpesi.CORE.Models.Users;
using RioUpesi.CORE.Response;
using RioUpesi.CORE.ViewModel;
using RioUpesi.CORE.ViewModel.User;
using RiUpesi.INFRASTRUCTURE.Iservices.IUserServices;

namespace RioUpesi.Controllers
{

    [Route("api/[controller]", Name = "User")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserServices _userservices;

       
        public UserController(IUserServices userservices)
            {
              _userservices= userservices;
            }


        [Route("Authenticate")]
        [HttpPost]
        public async Task<authenticationResponses> Authenticate(UserLogin loggedinuser)
            {
            var  res= await  _userservices.Authenticate(loggedinuser);
             return res;
            }


        [Authorize]
        [Route("RemoveUser")]
        [HttpPost]
        public async Task<string> RemoveUser(string userID)
            {
            var res= await _userservices.RemoveUser(userID);
            return res;
            }

      
        [Route("AddUser")]
        [HttpPost]
        public async Task<string> AddRioUser(AddUserVm vm)
            {
            var res= await _userservices.AddRioUser(vm);
            return res;
            }


        [Authorize]
        [Route("UserProfile")]
        [HttpGet]
        public async Task<RioUsers> UserProfile()
            {
            var res= await _userservices.UserProfile();
            return res;
            }
    }
}
