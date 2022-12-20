using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.Services;

namespace SimpleAppLoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfile _profileApi;
        private readonly IEmployee _employeeApi;    
        public ProfileController(IProfile profile, IEmployee employeeApi)
        {
            _profileApi = profile;
            _employeeApi = employeeApi;
        }

        [HttpGet("Get/Id/{id}")]
        public  async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _profileApi.Get(id);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _profileApi.GetAll();
            return Ok(result);
        }
        [HttpGet("GetBase64ProfilePicture/{empId}")]
        public async Task<IActionResult> GetBase64ProfilePicture([FromRoute] int empId)
        {
            var srtBase64URL = string.Empty;
            var result = await _profileApi.Get(empId);
            if (result != null)
            {
                srtBase64URL = "data:image/png;base64," + Convert.ToBase64String(result.ProfilePicture);
            }
            return Ok(srtBase64URL);
        }
        [HttpGet("GetProfilePicture/{empId}")]
        public async Task<IActionResult> GetProfilePicture([FromRoute] int empId)
        {
            var stream = new MemoryStream();
            var result = await _profileApi.Get(empId);
            if (result != null)
            {
                stream = new MemoryStream(result.ProfilePicture);  
            }
            return base.File(stream, "image/png");
        }

       

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id    )
        {
            var result = await _profileApi.Delete(id);
            return Ok(result);
        }

        [HttpPost("Upload/{empId}")]
        public async Task<IActionResult> Upload([FromRoute] int empId)
        {
            try
            {
                if (Request.Form.Files.Any())
                {
                    var employeeInfo = await _employeeApi.Get(empId);
                    byte[] imageBytes;
                    if (employeeInfo != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            Request.Form.Files[0].CopyTo(ms);
                            imageBytes = ms.ToArray();
                        }
                        var profileInfo = new Profile();
                        profileInfo.EmpId = employeeInfo.Id;
                        profileInfo.ProfilePicture = imageBytes;
                        await _profileApi.AddOrUpdate(profileInfo);
                    }

                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
