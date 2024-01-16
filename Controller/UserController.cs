using JwtPracticePart2.DTO;
using JwtPracticePart2.Model;
using JwtPracticePart2.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtPracticePart2.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<ServiceResponseModel<TokenPost>> Login([FromBody] UserPost returnedUser){

            ServiceResponseModel<TokenPost> serviceResponse = _userService.Login(returnedUser);

            if(serviceResponse.Data == null){
                return NotFound(serviceResponse);
            }
            else{
                return Ok(serviceResponse);
            }
            
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<ServiceResponseModel<UserPost>> SignUp([FromBody] UserPost newUser){

            ServiceResponseModel<UserPost> serviceResponse = _userService.Register(newUser);

            if(serviceResponse.Data == null){
                return BadRequest(serviceResponse);
            }
            else{
                return Ok(serviceResponse);
            }
        }

        [AllowAnonymous]
        [HttpGet("users")]
        public ActionResult getUsers(){
            var listofUsers = _userService.users();
            return Ok(listofUsers);
        }

    }
}