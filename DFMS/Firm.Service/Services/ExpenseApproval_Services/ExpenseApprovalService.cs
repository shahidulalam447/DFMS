using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Firm.Service.Services.ExpenseApproval_Services
{
    public class ExpenseApprovalService : IExpenseApprovalService
    {
        public readonly FirmDBContext context;
        public ExpenseApprovalService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<ExpenseApprovalServiceVM> AddNewExpenseApproval(ExpenseApprovalServiceVM model)
        {
            try
            {
                ExpenseApproval expense = new ExpenseApproval();
                expense.ExpenseDate = model.ExpenseDate;
                expense.RefNo = model.RefNo;
                expense.ExpenseItemId = model.ExpenseItemId;
                expense.PaymentModeId = model.PaymentModeId;
                expense.Amount = model.Amount;
                expense.Description = model.Description;
                expense.IsComplete = model.IsComplete;
                expense.CreatedOn = DateTime.Now;
                expense.IsActive = true;
                context.ExpenseApproval.Add(expense);
                var exp = await context.SaveChangesAsync();
                if (exp != 0)
                {
                    model.Id = expense.Id;
                }
                return model;

            }
            catch
            (Exception ex)
            {
                return model;
            }
        }
        public async  Task<ExpenseApprovalServiceVM>AddNewExpenseList(ExpenseApprovalServiceVM model)
        {
            try 
            {
                var expenselist = new ExpenseApproval();
                expenselist.ExpenseDate = model.ExpenseDate;
                expenselist.RefNo = model.RefNo;
                expenselist.ExpenseItemId = model.ExpenseItemId;
                expenselist.Amount = model.Amount;
                expenselist.IsComplete = model.IsComplete;
                expenselist.CreatedOn = DateTime.Now;
                expenselist.IsActive = true;
                context.ExpenseApproval.Add(expenselist);
                var exp = await context.SaveChangesAsync();
                if (exp != 0)
                {
                    model.Id = expenselist.Id;
                }
                return model;

            }
            catch
            (Exception ex)
            {
                return model;
            }
        }
        public async Task<List<ExpenseApprovalServiceVM>> GetAll()
        {
            List<ExpenseApprovalServiceVM> lists = new List<ExpenseApprovalServiceVM>();
            var data = await context.ExpenseApproval.Where(x => x.IsActive).ToListAsync();
            foreach (var expense in data)
            {
                ExpenseApprovalServiceVM model = new ExpenseApprovalServiceVM();
                model.Id = expense.Id;
                model.ExpenseDate = expense.ExpenseDate;
                model.RefNo = expense.RefNo;
                model.ExpenseItemId = expense.ExpenseItemId;
                model.PaymentModeId = expense.PaymentModeId;
                model.Status = expense.Status;
                model.Amount = expense.Amount;
                model.Description = expense.Description;
                model.IsComplete = expense.IsComplete;
                lists.Add(model);
            }
            return lists;
        }
        public async Task<ExpenseApprovalServiceVM> GetById(long id)
        {
            var expense = await context.ExpenseApproval.FindAsync(id);
            ExpenseApprovalServiceVM model = new ExpenseApprovalServiceVM();
            if(expense != null)
            {
                model.Id = expense.Id;
                model.ExpenseDate = expense.ExpenseDate;
                model.RefNo= expense.RefNo;
                model.ExpenseItemId= expense.ExpenseItemId;
                model.PaymentModeId= expense.PaymentModeId;
                model.Status = expense.Status;
                model.Amount = expense.Amount;
                model.Description = expense.Description;
                model.IsComplete = expense.IsComplete;
            }
            return model;
        }
        public async Task<ExpenseApprovalServiceVM> UpdateExpense(ExpenseApprovalServiceVM model)
        {
            try
            {
                var expense = await context.ExpenseApproval.FindAsync(model.Id);
                expense.ExpenseDate = model.ExpenseDate;
                expense.RefNo = model.RefNo;
                expense.ExpenseItemId = model.ExpenseItemId;
                expense.PaymentModeId = model.PaymentModeId;
                expense.Amount = model.Amount;
                expense.Description = model.Description;
                expense.IsComplete = model.IsComplete;
                expense.CreatedOn = DateTime.Now;
                expense.IsActive = true;
                expense.UpdatedOn = DateTime.Now;
                context.Entry(expense).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch
            (Exception ex)
            {
                return model;
            }
        }
        public async Task<bool> Remove(long id)
        {
            var expense = await context.ExpenseApproval.FindAsync(id);
            expense.IsActive = false;
            context.Entry(expense).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CompleteExpense(long id)
        {
            var expense = await context.ExpenseApproval.FindAsync(id);
            expense.IsComplete = true;
            context.Entry(expense).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
    }
    }

