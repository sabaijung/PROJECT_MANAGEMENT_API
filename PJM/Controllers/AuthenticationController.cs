using Microsoft.AspNetCore.Mvc;
using PJM.Models.Queries;
using PJM.Models.Request;
using System;
using System.Threading.Tasks;

namespace PJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationQ _authenticationQ = new AuthenticationQ();
        [HttpPost("Authen")]
        public async Task<IActionResult> Login([FromBody] AuthenticationReq login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    var result = await _authenticationQ.Login(login, url);
                    return StatusCode(200, result);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
    }
}
