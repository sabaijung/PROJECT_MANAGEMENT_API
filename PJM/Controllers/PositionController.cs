using Microsoft.AspNetCore.Mvc;
using PJM.Models.Queries;
using System;
using System.Threading.Tasks;

namespace PJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly PositionQ positionQ = new PositionQ();
        [HttpGet("GetPosition")]
        public async Task<IActionResult> GetPosition(int deptCode)
        {
            try
            {
                return Ok(await positionQ.GetAllPosition(deptCode));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
    }
}
