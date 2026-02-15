using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedConsumptionBulk_Services
{
    public interface IFeedConsumptionBulkService
    {
        Task<FeedConsumptionBulkServiceVM> AddNewFeedConsumptionBulk(FeedConsumptionBulkServiceVM model);
        Task<List<FeedConsumptionBulkServiceVM>> GetAll();
        Task<FeedConsumptionBulkServiceVM> GetById(long id);
        Task<FeedConsumptionBulkServiceVM> UpdateFeedConsumptionBulk(FeedConsumptionBulkServiceVM model);
        Task<List<FeedConsumptionBulkServiceVM>> ShadeLineFeedList();
        Task<bool> Remove(long id);
    }
}
