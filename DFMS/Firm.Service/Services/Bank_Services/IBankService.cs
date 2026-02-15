using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Bank_Services
{
    public interface IBankService
    {
        Task<BankServiceVM> AddNewBank(BankServiceVM model);
        Task<List<BankServiceVM>> GetAll();
        Task<BankServiceVM> GetById(long id);
        Task<BankServiceVM> UpdateBank(BankServiceVM model);
        Task<bool> SoftDelete(long id);
        Task<bool> Undo(long id);
        Task<bool> Remove(long id);
    }
}
