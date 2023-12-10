using Blazorise.Extensions;
using Blog.Logic.Dto.UserDtos;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("/BasicsUsers")]
        [HttpGet]
        public IEnumerable<AdminUserManagementDto> GetBasicUsers()
        {
            var result = _userService.GetAllNormalUsers();
            return result;
        }


        [Route("/AllUsers")]
        [HttpGet]
        public IEnumerable<UserDto> GetAllUsers()
        {
            var result = _userService.GetAll<UserDto>();
            
            return result;
        }


         [Route("/User")]
         [HttpGet]
         public UserDto GetUser([FromQuery] string Id)
         {
             var result = _userService.GetUserById<UserDto>(Guid.Parse(Id));
             return result;
        }


        [Route("/UserEmail")]
        [HttpGet]
        public UserDto GetUserByEmail([FromQuery] string email)
        {
            var result = _userService.GetUserByContainedString<UserDto>(email);
            return result;
        }


        [Route("/SendUser")]
        [HttpPost]
        public ActionResult<UserDto> Post([FromBody] UserDto user)
        {
            var result = _userService.Add(user);

            return result.IsCompletedSuccessfully ? Ok(user) : BadRequest();
        }


        [Route("/ChangePassword")]
        [HttpPut]
        public ActionResult Put([FromBody] PasswordUserDto user)
        {
            try
            {
                var result = _userService.ChangePassword(user);

                return result.IsCompletedSuccessfully ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Route("/DeleteAccount")]
        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteUserApiDto user) 
        {
            try
            {
                var serviceUser = _userService.GetUserByContainedString<UserDto>(user.Email);
                var result = _userService.SoftDelete(user.Password, serviceUser);

                return result.IsCompletedSuccessfully ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
