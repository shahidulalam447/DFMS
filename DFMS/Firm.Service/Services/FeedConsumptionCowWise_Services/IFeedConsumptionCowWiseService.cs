using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedConsumptionCowWise_Services
{
    public interface IFeedConsumptionCowWiseService
    {
        Task<FeedConsumptionCowWiseServiceVM> AddNewFeedConsumptionCowWiseService(FeedConsumptionCowWiseServiceVM model);
        Task<List<FeedConsumptionCowWiseServiceVM>> GetAll();
        Task<FeedConsumptionCowWiseServiceVM> GetById(long id);
        Task<FeedConsumptionCowWiseServiceVM> UpdateFeedConsumptionCowWiseService(FeedConsumptionCowWiseServiceVM model);
        Task<bool> Remove(long id);
    }
}
