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


        public async Task<object> GetUsers(int pageSize = 10, int currentPage = 1, string search = "")
        {
            var query =  context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Name.Contains(search));
            }

            int count = query.Count();
            var item = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            var result = (from a in item
                          select new
                          {
                              a.Code,
                              a.Name,
                              a.Lastname,
                              a.Username,
                              a.Isused

                          });

            return new
            {
                StatusCode = count == 0 ? "000" : "001",
                Message = count == 0 ? "ไม่พบข้อมูล" : "พบข้อมูล",
                Pagin = new
                {
                    totlalPage = (int)Math.Ceiling((double)count / pageSize),
                    totalRow = count,
                    currentPage,
                    pageSize
                },
                Data = result
            };
            
        }

        public async Task<object> GetUsers1()
        {
            var value = await context.Users.Where(a => a.Isused == "1").Select(a => new
            {
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


        public async Task<object> UpdateUsers(string code, UserReq user)
        {
            User data = await context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Code.Equals(code));
            if (data == null) return new { StatusCode = 200, taskStatus = false, Message = "ไม่พบข้อมูลผู้ใช้นี้" };

            /* string filename = "";
             if (user.ImageProfile != null)
             {
                 string WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Profile\\");
                 string uploads = Path.Combine(WebRootPath);
                 if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                 filename = Guid.NewGuid().ToString() + "." + user.ImageProfile.ContentType.Split("/")[1];
                 var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create);
                 await user.ImageProfile.CopyToAsync(fileStream);
             }
            */

            data.Code = code;
            data.InitialCode = user.InitialCode;
            data.Name = user.Name;
            data.Lastname = user.Lastname;
            data.DepartmentCode = user.DepartmentCode;
            data.PositionCode = user.PositionCode;
            data.Mobilephone = user.Mobilephone;
            data.Address = user.Address;
            data.ProvinceCode = user.ProvinceCode;
            data.AmphurCode = user.AmphurCode;
            data.DistrictCode = user.DistrictCode;
            data.Postcode = user.Postcode;
            // data.ImageProfile = user.ImageProfile != null ? filename : context.Users.AsNoTracking().First(a => a.Code == code).ImageProfile;
            data.Username = user.Username;
            data.Password = user.Password;
            data.Isused = user.Isused;
            data.Role = user.Role;

            context.Entry(data).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return new { StatusCode = 200, taskStatus = true, Message = "บันทึกสำเร็จ" };
        }

        public async Task<object> DeleteUsers(string code)
        {
            User validUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Code.Equals(code));
            if (validUser == null) return new { StatusCode = 200, taskStatus = false, Message = "ไม่พบข้อมูลผู้ใช้นี้" };

            validUser.Isused = "0";
            context.Entry(validUser).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return new { StatusCode = 200, taskStatus = true, Message = "บันทึกสำเร็จ" };
        }

        public async Task<object> GetUserDetail(string code)
        {

            var data = await context.Users.AsNoTracking().Select(a => new
            {
                a.Code,
                a.Name,
                a.Lastname,
                a.Username,
                a.Mobilephone,
                a.DepartmentCode,
                a.PositionCode,
                a.Address,
                a.DistrictCode,
                a.AmphurCode,
                a.ProvinceCode,
                a.Postcode,
                a.Role,
                a.Isused,
            }).FirstOrDefaultAsync(a => a.Code == code);

            if (data == null) return new { StatusCode = 200, taskStatus = false, Message = "ไม่พบข้อมูลผู้ใช้นี้" };

            return new { StatusCode = 200, Message = "สำเร็จ", Data = data };
        }
    }
}
