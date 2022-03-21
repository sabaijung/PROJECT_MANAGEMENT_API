using Microsoft.AspNetCore.Mvc;
using PJM.Models.Data;
using PJM.Models.Queries;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectQ projectQ = new ProjectQ();
        [HttpGet("GetProjects")]
        public async Task<IActionResult> GetProjects(int pageSize = 10, int currentPage = 1, string search = "")
        {
            try
            {
                return Ok(await projectQ.GetProject(pageSize, currentPage, search));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateUser([FromForm] Project p)
        {
            try
            {
                return Ok(await projectQ.CreateProject(p));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }


        [HttpPut("UpdateProject/{code}")]
        public async Task<IActionResult> UpdateUser([Required] int code, [FromForm] Project p)
        {
            try
            {
                return Ok(await projectQ.UpdateProject(code, p));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteUsers(int code)
        {
            try
            {
                var result = await projectQ.DeleteProject(code);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
    }
}
