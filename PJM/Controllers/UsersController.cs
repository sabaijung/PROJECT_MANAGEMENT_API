﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await usersQ.GetUsers());
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
        public async Task<IActionResult> DeleteUsers([Required] string code)
        {
            try
            {
                var result = await usersQ.DeleteUsers(code);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
    }
}
