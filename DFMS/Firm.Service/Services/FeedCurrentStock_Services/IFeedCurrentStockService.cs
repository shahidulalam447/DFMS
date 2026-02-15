using Firm.Service.Services.FeedEntry_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedCurrentStock_Services
{
    public interface IFeedCurrentStockService
    {
        Task<List<FeedCurrentStockServiceVM>> GetAll();
        Task<FeedCurrentStockServiceVM> GetById(long id);

    }
}
