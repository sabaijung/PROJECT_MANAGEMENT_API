using System;
using System.Collections.Generic;

#nullable disable

namespace PJM.Models.Data
{
    public partial class Position
    {
        public int PositionCode { get; set; }
        public string PositionName { get; set; }
        public int? DepartmentCode { get; set; }
        public string Isused { get; set; }
    }
}
