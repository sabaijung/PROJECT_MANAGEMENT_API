using System;
using System.Collections.Generic;

#nullable disable

namespace PJM.Models.Data
{
    public partial class User
    {
        public string Code { get; set; }
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
        public string ImageProfile { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Isused { get; set; }
        public string Role { get; set; }
    }
}
