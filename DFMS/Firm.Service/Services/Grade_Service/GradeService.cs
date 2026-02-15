using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firm.Service.Services.Grade_Service
{
    public class GradeService : IGradeService
    {
        private readonly FirmDBContext context;
        public GradeService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<GradeServiceVM> AddNewGrade(GradeServiceVM model)
        {
            bool isExists = await context.Grades.AnyAsync(c => c.GradeNo == model.GradeNo);
            if (isExists)
            {
                model.ErrorMessage = "Grade already exists. Please add a unique Grade.";
                return model;
            }
            try
            {
                Grade grade = new Grade();
                grade.GradeNo = model.GradeNo;
                grade.Designation = model.Designation;
                grade.MinSalary = model.MinSalary;
                grade.MaxSalary = model.MaxSalary;
                grade.IncrementAmount = model.IncrementAmount;
                grade.IncrementNumber = model.IncrementNumber;
                grade.CreatedOn = DateTime.Now;
                //grade.IsActive = true;
                grade.IsDeleted = false;
                context.Grades.Add(grade);
                var res = await context.SaveChangesAsync();
                if (res != 0)
                {
                    model.Id = grade.Id;
                }

                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new grade. Details:" + ex.Message;
                return model;
            }
        }

        public async Task<List<GradeServiceVM>> GetAll()
        {
            List<GradeServiceVM> lists = new List<GradeServiceVM>();
            var data = await context.Grades.ToListAsync();
            foreach (var grade in data)
            {
                GradeServiceVM model = new GradeServiceVM();
                model.Id = grade.Id;
                model.GradeNo = grade.GradeNo;
                model.Designation = grade.Designation;
                model.MinSalary = grade.MinSalary;
                model.MaxSalary = grade.MaxSalary;
                model.IncrementAmount = grade.IncrementAmount;
                model.IncrementNumber = grade.IncrementNumber;
                model.IsActive = grade.IsActive;
                model.IsDeleted = grade.IsDeleted;
                lists.Add(model);
            }
            return lists;
        }

        public async Task<GradeServiceVM> GetById(long id)
        {
            var grade = await context.Grades.FindAsync(id);
            GradeServiceVM model = new GradeServiceVM();
            if (grade != null)
            {
                model.GradeNo = grade.GradeNo;
                model.Designation = grade.Designation;
                model.MinSalary = grade.MinSalary;
                model.MaxSalary = grade.MaxSalary;
                model.IncrementAmount = grade.IncrementAmount;
                model.IncrementNumber = grade.IncrementNumber;
                model.Id = id;
            }
            return model;
        }
        public async Task<GradeServiceVM> UpdateGrade(GradeServiceVM model)
        {
            bool isExists = await context.Grades.AnyAsync(c => c.GradeNo == model.GradeNo && c.Id != model.Id);
            if (isExists)
            {
                model.ErrorMessage = "Grade already exists. Please add another Grade No";
                return model;
            }

            try
            {
                var grade = await context.Grades.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (grade != null)
                {
                    grade.GradeNo = model.GradeNo;
                    grade.Designation = model.Designation;
                    grade.MinSalary = model.MinSalary;
                    grade.MaxSalary = model.MaxSalary;
                    grade.IncrementAmount = model.IncrementAmount;
                    grade.IncrementNumber = model.IncrementNumber;
                    grade.UpdatedOn = DateTime.Now;
                    context.Entry(grade).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new Grade. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<bool> SoftDelete(long id)
        {
            try
            {
                var grade = await this.context.Grades.SingleOrDefaultAsync(x => x.Id == id);
                if (grade != null)
                {
                    grade.IsDeleted = true;
                    this.context.Entry(grade).State = EntityState.Modified;
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
                var grade = await this.context.Grades.SingleOrDefaultAsync(x => x.Id == id);
                if (grade != null)
                {
                    grade.IsDeleted = false;
                    this.context.Entry(grade).State = EntityState.Modified;
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
            var grade=await context.Grades.SingleOrDefaultAsync(x=>x.Id == id);
            if(grade != null)
            {
                context.Grades.Remove(grade);
                await context.SaveChangesAsync();
                return true;
            }
            return false;  
        }
    }
}
