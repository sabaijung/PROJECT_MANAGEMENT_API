using System;
using System.Collections.Generic;

#nullable disable

namespace PJM.Models.Data
{
    public partial class Project
    {
        public int Code { get; set; }
        public string ProjectName { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Detail { get; set; }
        public string ProjectStatus { get; set; }
    }
}
