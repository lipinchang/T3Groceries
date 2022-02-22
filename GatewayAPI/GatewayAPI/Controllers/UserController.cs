using GatewayAPI.Models;
using GatewayAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IManageUser<UserDTO> _manageUser;
        private readonly UserRepo _userRepo;

        public UserController(IManageUser<UserDTO> manageUser, UserRepo userRepo)
        {
            _manageUser = manageUser;
            _userRepo = userRepo;

        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserDTO user)
        {
            var myUser= await _manageUser.Add(user);

            if (myUser == null)
                return BadRequest("Could not register user");
            return Ok(myUser);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserDTO user)
        {
            var myUser = await _manageUser.Login(user);

            if (myUser == null)
                return BadRequest("Invalid Username or Password");
            return Ok(myUser);
        }

        [Route("GetUser")]
        [HttpGet]
        public async Task<ActionResult<User>> FindUser(string username)   //by username
        {
            var myUser = await _userRepo.GetUserDetail(username);
            if (myUser == null)
                return BadRequest("No such User");
            return Ok(myUser);
        }
    }
}
