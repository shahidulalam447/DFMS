using Firm.Infrastructure.Data;
using Firm.Service.Services.Vaccine_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.ExpenseApproval_Services
{
   public interface IExpenseApprovalService
    {
        Task<ExpenseApprovalServiceVM> AddNewExpenseApproval(ExpenseApprovalServiceVM model);
        Task<ExpenseApprovalServiceVM> AddNewExpenseList(ExpenseApprovalServiceVM model);
        Task<List<ExpenseApprovalServiceVM>> GetAll();
        Task<ExpenseApprovalServiceVM> GetById(long id);
        Task<ExpenseApprovalServiceVM> UpdateExpense(ExpenseApprovalServiceVM model);
        Task<bool> Remove(long id);
        Task<bool> CompleteExpense(long id);
    }
}
