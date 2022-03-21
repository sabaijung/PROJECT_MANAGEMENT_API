using Microsoft.AspNetCore.Mvc;
using PJM.Models.Queries;
using System;
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
    }
}
