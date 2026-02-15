using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedCategory_Services
{
    public interface IFeedCategoryService
    {
        Task<FeedCategoryServiceVM> AddNewFeedCategory(FeedCategoryServiceVM model);
        Task<List<FeedCategoryServiceVM>> GetAll();
        Task<List<FeedCategoryServiceVM>> GetAllWithStock();
        Task<FeedCategoryServiceVM> GetById(long id);
        Task<FeedCategoryServiceVM> UpdateFeedCategory(FeedCategoryServiceVM model);
        Task<bool> Remove(long id);
    }
}
