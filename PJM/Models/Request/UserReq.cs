using Microsoft.AspNetCore.Http;

namespace PJM.Models.Request
{
    public class UserReq
    {
        public string InitialCode { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string DepartmentCode { get; set; }
        public string PositionCode { get; set; }
        public string Mobilephone { get; set; }
        public string Address { get; set; }
        public string ProvinceCode { get; set; }
        public string AmphurCode { get; set; }
        public string DistrictCode { get; set; }
        public string Postcode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Isused { get; set; }
        public string Role { get; set; }
        public IFormFile ImageProfile { get; set; }
    }
}
