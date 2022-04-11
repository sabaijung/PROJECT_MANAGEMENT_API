using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PJM.Models.Data;
using PJM.Models.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PJM.Models.Queries
{
    public class AuthenticationQ
    {
        private readonly PROJECT_MANAGEMENTContext context = new PROJECT_MANAGEMENTContext();

        public string GenerateAccessToken(string Code, string userType, int minute = 60)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PJMHelpSecretKey"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new(JwtRegisteredClaimNames.Sub, Code));
            claims.Add(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new(ClaimTypes.Role, userType));

            var token = new JwtSecurityToken(
                  "https://localhost:44318",
                  "https://localhost:44318",
                  notBefore: DateTime.Now,
                  expires: DateTime.Now.AddMinutes(minute),
                  claims: claims,
                  signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<object> Login(AuthenticationReq authen, string url)
        {
            var user = await context.Users.Where(u => u.Username == authen.Username 
                    && u.Password == authen.Password && u.Isused == "1").Select(a => new
            {
                a.Code,
                a.Name,
                a.Lastname,
                a.Role
               // Role = a.UserType == "0" ? "user" : "admin"
               // ImgUrl = !string.IsNullOrEmpty(a.UserImage) ? url + "/UserImage/" + a.UserImage : ""

            }).FirstOrDefaultAsync();

            if (user != null)
            {
                var userType = user.Role == "0" ? "user" : "admin";
                return new { StatusCode = 1, TaskStatus = true, Users = user, AccessToken = GenerateAccessToken(user.Code, userType, 1440) };
            }
            else
            {
                return new { StatusCode = 1, TaskStatus = false, Message = "ชื่อผู้ใช้ หรือรหัสผ่านไม่ถูกต้อง" };
            }
        }

    }
}
