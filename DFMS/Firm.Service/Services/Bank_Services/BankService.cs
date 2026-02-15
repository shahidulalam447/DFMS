using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Firm.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Bank_Services
{
    public class BankService : IBankService
    {
        private readonly FirmDBContext context;
        public BankService(FirmDBContext context)
        {
            this.context = context;
        }

        public async Task<BankServiceVM> AddNewBank(BankServiceVM model)
        {
            bool isExists = await context.BankModels.AnyAsync(c => c.Name == model.Name);
            if (isExists)
            {
                model.ErrorMessage = "Bank already exists. Please add a unique Bank.";
                return model;
            }
            try
            {
               BankModel bank = new BankModel();
                bank.Name = model.Name;
                bank.BankBranch = model.BankBranch;
                bank.CreatedBy = model.CreatedBy;
                bank.CreatedDate = model.CreatedDate;
                bank.DisburseMethod = model.DisburseMethod;
                bank.BankAccount = model.BankAccount;
                bank.CreatedOn=DateTime.Now;
                bank.IsDeleted = false;
                context.BankModels.Add(bank);
                var res=await context.SaveChangesAsync();
                if (res !=0)
                {
                    model.Id = bank.Id;
                }
                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new bank. Details:" + ex.Message;
                return model;
            }
        }

        public async Task<List<BankServiceVM>> GetAll()
        {
            List<BankServiceVM> lists = new List<BankServiceVM>();
            var data=await context.BankModels.ToListAsync();
            foreach (var bank in data)
            {
                BankServiceVM model= new BankServiceVM();
                model.Id=bank.Id;
                model.Name = bank.Name;
                model.BankBranch = bank.BankBranch;
                model.CreatedBy = bank.CreatedBy;
                model.CreatedDate = bank.CreatedDate;
                model.DisburseMethod= bank.DisburseMethod;
                model.BankAccount = bank.BankAccount;
                model.IsActive = bank.IsActive;
                model.IsDeleted = bank.IsDeleted;
                lists.Add(model);
            }
            return lists;
        }

        public async Task<BankServiceVM> GetById(long id)
        {
            var bank = await context.BankModels.FindAsync(id);
            BankServiceVM model = new BankServiceVM();
            if (bank != null)
            {
                model.Name = bank.Name;
                model.BankBranch = bank.BankBranch;
                model.CreatedBy = bank.CreatedBy;
                model.CreatedDate = bank.CreatedDate;
                model.DisburseMethod= bank.DisburseMethod;
                model.BankAccount = bank.BankAccount;
                model.Id = id;
            }
            return model;
        }

        public async Task<BankServiceVM> UpdateBank(BankServiceVM model)
        {
            bool isExists=await context.BankModels.AnyAsync(c=>c.Name==model.Name && c.Id !=model.Id);
            if (isExists)
            {
                model.ErrorMessage = "bank already exists. Please add another bank name";
                return model;
            }
            try
            {
                var bank = await context.BankModels.FirstOrDefaultAsync(c=>c.Id==model.Id);
                if (bank != null)
                {
                    bank.Name = model.Name;
                    model.BankBranch= bank.BankBranch;
                    bank.CreatedBy = model.CreatedBy;
                    bank.CreatedDate = model.CreatedDate;
                    bank.DisburseMethod = model.DisburseMethod;
                    bank.BankAccount = model.BankAccount;
                    bank.UpdatedOn=DateTime.Now;
                    context.Entry(bank).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new Bank. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> SoftDelete(long id)
        {
            try
            {
                var bank=await this.context.BankModels.SingleOrDefaultAsync(x=>x.Id==id);
                if (bank != null)
                {
                    bank.IsDeleted= true;
                    this.context.Entry(bank).State = EntityState.Modified;
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
                var bank = await this.context.BankModels.FirstOrDefaultAsync( x=>x.Id==id);
                if (bank != null)
                {
                    bank.IsDeleted= false;
                    this.context.Entry(bank).State = EntityState.Modified;
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
            var bank=await context.BankModels.FirstOrDefaultAsync(x=>x.Id==id);
            if (bank !=null)
            {
                context.BankModels.Remove(bank);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
