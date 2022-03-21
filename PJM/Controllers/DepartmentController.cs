using Microsoft.AspNetCore.Mvc;
using PJM.Models.Queries;
using System;
using System.Threading.Tasks;

namespace PJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentQ departmentQ = new DepartmentQ();
        [HttpGet("GetAllDeparment")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                return Ok(await departmentQ.GetAllDepartment());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
    }
}
