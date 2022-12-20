using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAppLoginAPI.Context;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.ViewModels;

namespace SimpleAppLoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _userApi;

        public UserController(IUsers userApi)
        {
            _userApi = userApi;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) 
        {
            var result = await _userApi.Get(id); 
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userApi.GetAll();
            return Ok(result);
        }
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] User user)
        {
            var userFromDb = await _userApi.Get(user.Id);
            if(userFromDb != null)
            {
                var result = await _userApi.Update(userFromDb.Id, user);
                return Ok(result);
            }
            else
            {
                var result = await _userApi.Create(user);
                return Ok(result);
            }            
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _userApi.Delete(id);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel filter)
        {
            var result = await _userApi.Login(filter);   
            return Ok(result);
        }

        //[HttpPost("authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] User userObj)
        //{
        //    if (userObj == null)
        //        return BadRequest();

        //    var user = await _simpleContext.Users.
        //        FirstOrDefaultAsync(x => x.UserName == userObj.UserName && x.Password == userObj.Password);
        //    if (user == null)
        //        return NotFound(new {Message = "User Not Found!"});

        //    return Ok(new
        //    {
        //        Message = "Login Success!"
        //    });    

        //}
    }
}
