using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Cow_Services
{
    public interface ICowService
    {
        Task<CowServiceViewModel> AddNewCow(CowServiceViewModel model);
        Task<List<CowServiceViewModel>> GetAll();
        Task<CowServiceViewModel> GetById(long id);
        Task<CowServiceViewModel> UpdateCow(CowServiceViewModel model);
        Task<bool> Remove(long id);
        Task<CowServiceViewModel> GetCowHistoryById(long id);
        Task<CowSummaryVM> CowSummary30Days();
    }
}
