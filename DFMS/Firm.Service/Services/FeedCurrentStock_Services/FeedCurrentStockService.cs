using Firm.Infrastructure.Data;
using Firm.Service.Services.FeedEntry_Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedCurrentStock_Services
{
    public class FeedCurrentStockService : IFeedCurrentStockService
    {
        private readonly FirmDBContext context;

        public FeedCurrentStockService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<List<FeedCurrentStockServiceVM>> GetAll()
        {
            List<FeedCurrentStockServiceVM> lists = new List<FeedCurrentStockServiceVM>();
            var data = await context.FeedCurrentStocks.Where(x => x.IsActive).ToListAsync();
            foreach (var feedCurrentStock in data)
            {
                FeedCurrentStockServiceVM model = new FeedCurrentStockServiceVM();
                model.Date = feedCurrentStock.Date;
                model.FeedCategoryId = feedCurrentStock.FeedCategoryId;
                model.FeedCategoryName = feedCurrentStock.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedCurrentStock.FeedCategoryId).FeedCategoryName;
                model.CurrentQuantity = feedCurrentStock.CurrentQuantity;
                model.CurrentUnitPrice = feedCurrentStock.CurrentUnitPrice;
                model.IsActive = feedCurrentStock.IsActive;
                if(feedCurrentStock.UpdatedOn == null)
                {
                    model.UpdatedOn = feedCurrentStock.CreatedOn;
                }
                else
                {
                    model.UpdatedOn = feedCurrentStock.UpdatedOn;
                }
                model.Id = feedCurrentStock.Id;
                lists.Add(model);
            }

            return lists;
        }
        public async Task<FeedCurrentStockServiceVM> GetById(long id)
        {
            var feedCurrentStock = await context.FeedCurrentStocks.FirstOrDefaultAsync(c=>c.FeedCategoryId == id);
            FeedCurrentStockServiceVM model = new FeedCurrentStockServiceVM();
            if (feedCurrentStock != null)
            {
                model.Date = feedCurrentStock.Date;
                model.FeedCategoryId = feedCurrentStock.FeedCategoryId;
                model.FeedCategoryName = feedCurrentStock.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedCurrentStock.FeedCategoryId).FeedCategoryName;
                model.CurrentQuantity = feedCurrentStock.CurrentQuantity;
                model.CurrentUnitPrice = feedCurrentStock.CurrentUnitPrice;
                model.IsActive = feedCurrentStock.IsActive;
                if (feedCurrentStock.UpdatedOn == null)
                {
                    model.UpdatedOn = feedCurrentStock.CreatedOn;
                }
                else
                {
                    model.UpdatedOn = feedCurrentStock.UpdatedOn;
                }
                model.Id = feedCurrentStock.Id;
            }
            return model;
        }

    }
}
