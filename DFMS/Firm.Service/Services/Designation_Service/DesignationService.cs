//using Firm.Core.EntityModel;
//using Firm.Infrastructure.Data;
//using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Firm.Service.Services.Designation_Service
//{
//    public class DesignationService : IDesignationService
//    {
//        public readonly FirmDBContext context;
//        public DesignationService(FirmDBContext context)
//        {
//            this.context = context;
//        }

//        public async Task<DesignationServiceVM> AddNewDesignation(DesignationServiceVM model)
//        {
//            bool isExists = await context.Designations.AnyAsync(c => c.DesignationName == model.DesignationName);
//            if (isExists)
//            {
//                model.ErrorMessage = "Designation already exists. Please add a unique Designation.";
//                return model;
//            }
//            try
//            {
//                Designation designation = new Designation();
//                designation.DesignationName = model.DesignationName;
//                designation.Salary = model.Salary;
//                var res = await context.SaveChangesAsync();
//                if (res != 0)
//                {
//                    model.Id = designation.Id;
//                }
//                return model;
//            }
//            catch (Exception ex)
//            {

//                model.ErrorMessage = "Error occurred while adding a new Designation. Details: " + ex.Message;
//                return model;
//            }
//        }

//        public async Task<List<DesignationServiceVM>> GetAll()
//        {
//            List<DesignationServiceVM> lists = new List<DesignationServiceVM>();
//            var data = await context.Designations.Where(x => x.IsActive).ToListAsync();
//            foreach (var designation in data)
//            {
//                DesignationServiceVM model = new DesignationServiceVM();
//                model.DesignationName = designation.DesignationName;
//                model.Salary= designation.Salary;
//                model.Id = designation.Id;
//            }
//            return lists;
//        }

//        public async Task<DesignationServiceVM> GetById(long id)
//        {
//            var designation = await context.Designations.FindAsync(id);
//            DesignationServiceVM model = new DesignationServiceVM();
//            if (designation != null)
//            {
//                model.DesignationName = designation.DesignationName;
//                model.Id= designation.Id;
//            }
//            return model;
//        }
//        public async Task<DesignationServiceVM> UpdateDesignation(DesignationServiceVM model)
//        {
//            bool isExists = await context.Designations.AnyAsync(c => c.DesignationName == model.DesignationName && c.DesignationId != model.DesignationId);
//            if (isExists)
//            {
//                model.ErrorMessage = "Designation already exists. Please add another Designation Name.";
//                return model;
//            }
//            try
//            {
//                var designation = await context.Designations.FirstOrDefaultAsync(c => c.DesignationId == model.DesignationId);
//                if (designation != null)
//                {
//                    designation.DesignationName = model.DesignationName;
//                    context.Entry(designation).State = EntityState.Modified;
//                    await context.SaveChangesAsync();
//                }
//                return model;
//            }
//            catch (Exception ex)
//            {

//                model.ErrorMessage = "Error occurred while adding a new Designation. Details: " + ex.Message;
//                return model;
//            }
//        }
//        public async Task<bool> Remove(long id)
//        {
//            var designation = await context.Designations.FirstOrDefaultAsync(c => c.DesignationId == id);
//            designation.IsActive = false;
//            context.Entry(designation).State = EntityState.Modified;
//            await context.SaveChangesAsync();
//            return true;
//        }

//        public Task<DesignationServiceVM> AddNewDesignationService()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
