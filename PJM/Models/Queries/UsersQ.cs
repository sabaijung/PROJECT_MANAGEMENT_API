using Microsoft.EntityFrameworkCore;
using PJM.Models.Data;
using PJM.Models.Request;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PJM.Models.Queries
{
    public class UsersQ
    {
        private readonly PROJECT_MANAGEMENTContext context = new PROJECT_MANAGEMENTContext();


        public async Task<object> GetUsers()
        {
            var value = await context.Users.Where(a => a.Isused == "1").Select(a => new {
                a.Name,
                a.Lastname
            }).ToListAsync();

            return value;

            // ImageProfile = !string.IsNullOrEmpty(a.ImageProfile) ? "https://localhost:44317/" + "Profile/" + a.ImageProfile : ""
        }

        public string GenCode()
        {
            var lastId = context.Users.OrderByDescending(a => a.Code).FirstOrDefault();
            if (lastId != null)
            {
                int code = Convert.ToInt32(lastId.Code);
                return (code + 1).ToString();
            }
            return "1";
        }

        public async Task<object> CreateUsers(UserReq user)
        {
            //string filename = "";
            /*if (user.ImageProfile != null)
            {
                string WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Profile\\");
                string uploads = Path.Combine(WebRootPath);
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                filename = Guid.NewGuid().ToString() + "." + user.ImageProfile.ContentType.Split("/")[1];
                var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create);
                await user.ImageProfile.CopyToAsync(fileStream);
            }*/

            context.Users.Add(new User
            {
                Code = GenCode(),
                InitialCode = user.InitialCode,
                Name = user.Name,
                Lastname = user.Lastname,
                DepartmentCode = user.DepartmentCode,
                PositionCode = user.PositionCode,
                Mobilephone = user.Mobilephone,
                Address = user.Address,
                ProvinceCode = user.ProvinceCode,
                AmphurCode = user.AmphurCode,
                DistrictCode = user.DistrictCode,
                Postcode = user.PositionCode,
               // ImageProfile = user.ImageProfile != null ? filename : "",
                Username = user.Username,
                Password = user.Password,
                Isused = "1",
                Role = user.Role
            });
            await context.SaveChangesAsync();

            return new { StatusCode = 200, taskStatus = true, Message = "บันทึกสำเร็จ" };
        }
    }
}
