using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.Services;

namespace SimpleAppLoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ISignup _signUpApi;
        private readonly IUsers _userApi;
        public SignupController(ISignup signUpApi, IUsers userApi)
        {
            _signUpApi = signUpApi;
            _userApi = userApi;
        }

        [HttpGet("GetById/{id}")]        
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await _signUpApi.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId([FromRoute] int userId)
        {
            var result = await _signUpApi.GetByUserId(userId);    
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _signUpApi.GetAll();
            return Ok(result);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] Signup signUpUser)
        {
            var SignUpUserFromDb = await _signUpApi.GetById(signUpUser.Id);
            if(SignUpUserFromDb != null)
            {
                var result = await _signUpApi.Update(SignUpUserFromDb.Id, signUpUser);
                return Ok(result);
            }
            else
            {
                var result = await _signUpApi.Create(signUpUser);
                return Ok(result);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _signUpApi.Delete(id);
            return Ok(result);
        }


    }
}
