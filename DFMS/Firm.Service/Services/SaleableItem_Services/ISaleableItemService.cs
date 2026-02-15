using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.SaleableItem_Services
{
    public interface ISaleableItemService
    {
        Task<SaleableItemServiceVM> AddNewSaleableItem(SaleableItemServiceVM model);
        Task<List<SaleableItemServiceVM>> GetAll();
        Task<SaleableItemServiceVM> GetById(long id);
        Task<SaleableItemServiceVM> UpdateSaleableItem(SaleableItemServiceVM model);
        Task<bool> Remove(long id);
    }
}
