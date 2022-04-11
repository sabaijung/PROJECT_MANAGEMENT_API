using Microsoft.EntityFrameworkCore;
using PJM.Models.Data;
using PJM.Models.Request;
using System.Linq;
using System.Threading.Tasks;

namespace PJM.Models.Queries
{
    public class AuthenticationQ
    {
        private readonly PROJECT_MANAGEMENTContext context = new PROJECT_MANAGEMENTContext();

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
