using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;

namespace SimpleAppLoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeApi;

        public EmployeeController(IEmployee employeeApi)
        {
            _employeeApi = employeeApi;
        }

        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _employeeApi.Get(id);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeApi.GetAll();
            return Ok(result);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] Employee employee)
        {
            var empFromDb = await _employeeApi.Get(employee.Id);
            if (empFromDb != null)
            {
                var result = await _employeeApi.Update(empFromDb.Id, employee);
                return Ok(result.Id);
            }
            else
            {
                var result = await _employeeApi.Create(employee);
                return Ok(result.Id);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _employeeApi.Delete(id);
            return Ok(result);
        }
    }
}
