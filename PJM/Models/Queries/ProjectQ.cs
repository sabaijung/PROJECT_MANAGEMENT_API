using PJM.Models.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PJM.Models.Queries
{
    public class ProjectQ
    {
        private readonly PROJECT_MANAGEMENTContext context = new PROJECT_MANAGEMENTContext();

        public async Task<object> GetProject(int pageSize = 10, int currentPage = 1, string search = "")
        {
            var query = context.Projects.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.ProjectName.Contains(search));
            }

            int count = query.Count();
            var item = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            var result = (from a in item
                          select new
                          {
                              a.Code,
                              a.ProjectName,
                              a.ProjectStatus,
                              a.DateStart,
                              a.DateEnd
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
    }
}
