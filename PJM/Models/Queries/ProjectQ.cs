using Microsoft.EntityFrameworkCore;
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

        public async Task<object> CreateProject(Project p)
        {
            context.Projects.Add(new Project
            {
                ProjectName = p.ProjectName,
                Detail = p.Detail,
                ProjectStatus = p.ProjectStatus,
                DateStart = p.DateStart,
                DateEnd = p.DateEnd
               
            });
            await context.SaveChangesAsync();

            return new { StatusCode = 200, taskStatus = true, Message = "บันทึกสำเร็จ" };
        }

        public async Task<object> UpdateProject(int code, Project p)
        {
            Project data = await context.Projects.AsNoTracking().FirstOrDefaultAsync(a => a.Code.Equals(code));
            if (data == null) return new { StatusCode = 200, taskStatus = false, Message = "ไม่พบข้อมูลผู้ใช้นี้" };


            data.ProjectName = p.ProjectName;
            data.Detail = p.Detail;
            data.DateStart = p.DateStart;
            data.DateEnd = p.DateEnd;
            data.ProjectStatus = p.ProjectStatus;


            context.Entry(data).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return new { StatusCode = 200, taskStatus = true, Message = "บันทึกสำเร็จ" };
        }

        public async Task<object> DeleteProject(int code)
        {
            Project validProject = await context.Projects.AsNoTracking().FirstOrDefaultAsync(a => a.Code.Equals(code));
            if (validProject == null) return new { StatusCode = 200, taskStatus = false, Message = "ไม่พบข้อมูลผู้ใช้นี้" };

            validProject.ProjectStatus = "0";
            context.Entry(validProject).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return new { StatusCode = 200, taskStatus = true, Message = "บันทึกสำเร็จ" };
        }
    }
}
