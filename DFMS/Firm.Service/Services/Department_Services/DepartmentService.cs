using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Department_Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly FirmDBContext context;
        public DepartmentService(FirmDBContext context)
        {
            this.context = context;
        }

        public async Task<DepartmentServiceVM> AddNewDepartment(DepartmentServiceVM model)
        {
            bool isExists = await context.Departments.AnyAsync(c => c.DepartmentName == model.DepartmentName);
            if (isExists)
            {
                model.ErrorMessage = "Department already exists. Please add a unique Department.";
                return model;
            }
            try
            {
                Department department = new Department();
                department.DepartmentName = model.DepartmentName;
                department.CreatedOn = DateTime.Now;
                department.IsDeleted = false;
                context.Departments.Add(department);
                var res = await context.SaveChangesAsync();
                if (res != 0)
                {
                    model.Id = department.Id;
                }
                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new Department. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<DepartmentServiceVM>> GetAll()
        {
            List<DepartmentServiceVM> lists = new List<DepartmentServiceVM>();
            var data = await context.Departments.ToListAsync();
            foreach (var department in data)
            {
                DepartmentServiceVM model= new DepartmentServiceVM();
                model.Id = department.Id;
                model.DepartmentName = department.DepartmentName;
                model.IsActive=department.IsActive;
                model.IsDeleted=department.IsDeleted;
                lists.Add(model);
            }

            return lists;
        }

        public async Task<DepartmentServiceVM> GetById(long id)
        {
            var department = await context.Departments.FindAsync(id);
            DepartmentServiceVM model=new DepartmentServiceVM();
            if (department != null)
            {
                model.DepartmentName= department.DepartmentName;
                model.Id = id;
            }
            return model;
        }
        public async Task<DepartmentServiceVM> UpdateDepartment(DepartmentServiceVM model)
        {
            bool isExists=await context.Departments.AnyAsync(c=>c.DepartmentName==model.DepartmentName && c.Id !=model.Id);
            if (isExists)
            {
                model.ErrorMessage = "Department already exists. Please add another DepartmentName.";
                return model;
            }
            try
            {
                var department = await context.Departments.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (department != null)
                {
                    department.DepartmentName = model.DepartmentName;
                    department.UpdatedOn = DateTime.Now;
                    context.Entry(department).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new Department. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> SoftDelete(long id)
        {
            try
            {
                var department= await this.context.Departments.SingleOrDefaultAsync(c => c.Id == id);
                if (department != null)
                {
                    department.IsDeleted = true;
                    this.context.Entry(department).State= EntityState.Modified;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {

                return false;
            }
        }

        public async Task<bool> Undo(long id)
        {
            try
            {
                var department= await this.context.Departments.SingleOrDefaultAsync(x=>x.Id==id);
                if (department != null)
                {
                    department.IsDeleted = false;
                    this.context.Entry(department).State=EntityState.Modified;
                    await context.SaveChangesAsync();
                    return false;
                }
                return true;
            }
            catch 
            {

                return true;
            }
        }

        public async Task<bool> Remove(long id)
        {
            var department = await context.Departments.FirstOrDefaultAsync(c => c.Id == id);
            if (department !=null)
            {
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
