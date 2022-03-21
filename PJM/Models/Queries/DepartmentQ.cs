using Microsoft.EntityFrameworkCore;
using PJM.Models.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PJM.Models.Queries
{
    public class DepartmentQ
    {
        private readonly PROJECT_MANAGEMENTContext context = new PROJECT_MANAGEMENTContext();
        public async Task<object> GetAllDepartment()
        {
            var value = await context.Departments.Where(a => a.Isused == "1").Select(d => new
            {
                d.DepartmentCode,
                d.DepartmentName
            }).ToListAsync();

            return value;

         }

    }
}
