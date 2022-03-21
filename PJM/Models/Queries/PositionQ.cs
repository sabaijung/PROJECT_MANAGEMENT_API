using Microsoft.EntityFrameworkCore;
using PJM.Models.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PJM.Models.Queries
{
    public class PositionQ
    {
        private readonly PROJECT_MANAGEMENTContext context = new PROJECT_MANAGEMENTContext();
        public async Task<object> GetAllPosition(int deptCode)
        {
            var value = await context.Positions.Where(p => p.DepartmentCode == deptCode &&  p.Isused == "1").Select(pos => new
            {
                pos.PositionCode,
                pos.PositionName
            }).ToListAsync();

            return value;

         }

    }
}
