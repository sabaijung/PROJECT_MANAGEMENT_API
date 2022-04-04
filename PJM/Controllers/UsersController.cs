using Microsoft.AspNetCore.Mvc;
using PJM.Models.Queries;
using PJM.Models.Request;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersQ usersQ = new UsersQ();


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(int pageSize = 10, int currentPage = 1, string search = "")
        {
            try
            {
                return Ok(await usersQ.GetUsers(pageSize, currentPage, search));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserReq user)
        {
            try
            {
                return Ok(await usersQ.CreateUsers(user));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPut("UpdateUser/{code}")]
        public async Task<IActionResult> UpdateUser([Required] string code, [FromForm] UserReq user)
        {
            try
            {
                return Ok(await usersQ.UpdateUsers(code, user));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            try
            {
                var result = await usersQ.DeleteUsers(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> GetUserDetail(string code)
        {
            try
            {
                return Ok(await usersQ.GetUserDetail(code));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
    }
}
