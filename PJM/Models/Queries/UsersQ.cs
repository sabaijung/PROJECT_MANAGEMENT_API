using Microsoft.EntityFrameworkCore;
using PJM.Models.Data;
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
    }
}
