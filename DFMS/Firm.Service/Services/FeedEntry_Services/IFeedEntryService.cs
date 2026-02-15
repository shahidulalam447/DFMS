using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedEntry_Services
{
    public interface IFeedEntryService
    {
        Task<FeedEntryServiceVM> AddNewFeedEntry(FeedEntryServiceVM model);
        Task<List<FeedEntryServiceVM>> GetAll();
        Task<FeedEntryServiceVM> GetById(long id);
        Task<FeedEntryServiceVM> UpdateFeedEntry(FeedEntryServiceVM model);
        Task<bool> Remove(long id);

    }
}
