using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firm.Service.Services.JobInformation_Services
{
    public class JobInformationService : IJobInformationService
    {
        private readonly FirmDBContext context;
        public JobInformationService(FirmDBContext context)
        {
            this.context = context;
        }

        public async Task<JobInformationServiceVM> AddNewJobInformation(JobInformationServiceVM model)
        {
            bool isExists = await context.JobInformation.AnyAsync(c => c.JoiningDate == model.JoiningDate);
            if (isExists)
            {
                model.ErrorMessage = "Job Information already exists. Please add a unique Job Information.";
                return model;
            }
            try
            {
                JobInformation jobInformation = new JobInformation();
                jobInformation.JoiningDate = model.JoiningDate;
                jobInformation.ProbationEndDate = model.ProbationEndDate;
                jobInformation.PermanentDate = model.PermanentDate;
                jobInformation.Department = model.Department;
                jobInformation.Designation = model.Designation;
                jobInformation.EmployeeCategory = model.EmployeeCategory;
                jobInformation.JobStatus = model.JobStatus;
                jobInformation.ServiceType = model.ServiceType;
                jobInformation.OfficeType = model.OfficeType;
                jobInformation.Shift = model.Shift;
                jobInformation.Grade = model.Grade;
                jobInformation.CreatedOn=DateTime.Now;
                //jobInformation.IsActive=model.IsActive;
                jobInformation.IsDeleted = false;
                context.JobInformation.Add(jobInformation);
                var res= await context.SaveChangesAsync();
                if (res != 0)
                {
                    model.Id = jobInformation.Id;
                }
                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new JobInformation. Details:" + ex.Message;
                return model;
            }
        }

        public async Task<List<JobInformationServiceVM>> GetAll()
        {
            List<JobInformationServiceVM> lists = new List<JobInformationServiceVM> ();
            var data=await context.JobInformation.ToListAsync();
            foreach (var jobInformation in data)
            {
                JobInformationServiceVM model= new JobInformationServiceVM();
                model.Id = jobInformation.Id;
                model.JoiningDate = jobInformation.JoiningDate;
                model.ProbationEndDate = jobInformation.ProbationEndDate;
                model.PermanentDate = jobInformation.PermanentDate;
                model.Department=jobInformation.Department;
                model.Designation=jobInformation.Designation;
                model.EmployeeCategory=jobInformation.EmployeeCategory;
                model.JobStatus=jobInformation.JobStatus;
                model.ServiceType=jobInformation.ServiceType;
                model.OfficeType=jobInformation.OfficeType;
                model.Shift=jobInformation.Shift;
                model.Grade=jobInformation.Grade;
                model.IsActive=jobInformation.IsActive;
                model.IsDeleted=jobInformation.IsDeleted;
                lists.Add(model);
            }
            return lists;
        }

        public async Task<JobInformationServiceVM> GetById(long id)
        {
            var jobInformation = await context.JobInformation.FindAsync(id);
            JobInformationServiceVM model = new JobInformationServiceVM();
            if (jobInformation != null)
            {
                model.JoiningDate = jobInformation.JoiningDate;
                model.ProbationEndDate = jobInformation.ProbationEndDate;
                model.PermanentDate = jobInformation.PermanentDate;
                model.Department = jobInformation.Department;
                model.Designation = jobInformation.Designation;
                model.EmployeeCategory = jobInformation.EmployeeCategory;
                model.JobStatus = jobInformation.JobStatus;
                model.ServiceType = jobInformation.ServiceType;
                model.OfficeType = jobInformation.OfficeType;
                model.Shift = jobInformation.Shift;
                model.Grade = jobInformation.Grade;
                model.Id = jobInformation.Id;

            }
            return model;

        }

        public async Task<JobInformationServiceVM> UpdateJobInformation(JobInformationServiceVM model)
        {
            bool isExists=await context.JobInformation.AnyAsync(c=>c.JoiningDate==model.JoiningDate && c.Id !=model.Id);
            if (isExists)
            {
                model.ErrorMessage = "JobInformation already exists. Please add another Joinin Date";
                return model;
            }
            try
            {
                var jobInformation = await context.JobInformation.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (jobInformation != null)
                {
                    jobInformation.JoiningDate = model.JoiningDate;
                    jobInformation.ProbationEndDate = model.ProbationEndDate;
                    jobInformation.PermanentDate = model.PermanentDate;
                    jobInformation.Department = model.Department;
                    jobInformation.Designation = model.Designation;
                    jobInformation.EmployeeCategory = model.EmployeeCategory;
                    jobInformation.JobStatus = model.JobStatus;
                    jobInformation.ServiceType = model.ServiceType;
                    jobInformation.OfficeType = model.OfficeType;
                    jobInformation.Shift = model.Shift;
                    jobInformation.Grade = model.Grade;
                    jobInformation.CreatedOn = DateTime.Now;
                    context.Entry(jobInformation).State = EntityState.Modified;
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
                var jobInformation=await this.context.JobInformation.SingleOrDefaultAsync(c => c.Id == id);
                if (jobInformation != null)
                {
                    jobInformation.IsDeleted = true;
                    this.context.Entry(jobInformation).State= EntityState.Modified;
                    await this.context.SaveChangesAsync();
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
                var jobInformation=await this.context.JobInformation.SingleOrDefaultAsync(x=>x.Id == id);
                if (jobInformation != null)
                {
                    jobInformation.IsDeleted = false;
                    this.context.Entry(jobInformation).State=EntityState.Modified;
                    await this.context.SaveChangesAsync();
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
            var jobInformation=await context.JobInformation.SingleOrDefaultAsync(x=>x.Id==id);
            if (jobInformation != null)
            {
                context.JobInformation.Remove(jobInformation);
                await this.context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
