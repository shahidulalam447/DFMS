using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Firm.Service.Services.FeedCurrentStock_Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedCategory_Services
{
    public  class FeedCategoryService : IFeedCategoryService
    {
        private readonly FirmDBContext context;

        public FeedCategoryService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<FeedCategoryServiceVM> AddNewFeedCategory(FeedCategoryServiceVM model)
        {
            bool isExists = await context.FeedCategories.AnyAsync(c => c.FeedCategoryName == model.FeedCategoryName);
            if (isExists)
            {
                model.ErrorMessage = "Feed Category already exists. Please add a unique Feed Category.";
                return model;
            }
            try
            {
                FeedCategory feedCategory = new FeedCategory();
                feedCategory.FeedCategoryName = model.FeedCategoryName;
                feedCategory.UnitName = model.UnitName;
                feedCategory.CreatedOn = DateTime.Now;
                feedCategory.IsActive = true;
                context.FeedCategories.Add(feedCategory);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.Id = feedCategory.Id;
                }


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new feed Category. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<FeedCategoryServiceVM>> GetAll()
        {
            List<FeedCategoryServiceVM> lists = new List<FeedCategoryServiceVM>();
            var data = await context.FeedCategories.Where(x => x.IsActive).ToListAsync();
            foreach (var feedCategory in data)
            {
                FeedCategoryServiceVM model = new FeedCategoryServiceVM();
                model.FeedCategoryName = feedCategory.FeedCategoryName;
                model.UnitName = feedCategory.UnitName;
                model.IsActive = feedCategory.IsActive;
                model.Id = feedCategory.Id;
                lists.Add(model); 
            }

            return lists;
        }

        public async Task<List<FeedCategoryServiceVM>> GetAllWithStock()
        {
            List<FeedCategoryServiceVM> lists = new List<FeedCategoryServiceVM>();
            var data = await context.FeedCategories.Where(x => x.IsActive).ToListAsync();
            foreach (var feedCategory in data)
            {
                FeedCategoryServiceVM model = new FeedCategoryServiceVM();
                model.FeedCategoryName = feedCategory.FeedCategoryName;
                model.UnitName = feedCategory.UnitName;
                model.IsActive = feedCategory.IsActive;
                model.Id = feedCategory.Id;
                var stock = await context.FeedCurrentStocks.FirstOrDefaultAsync(x => x.IsActive && x.FeedCategoryId == feedCategory.Id);
                if (stock == null)
                {
                    model.feedCurrentStockServiceVM = new FeedCurrentStockServiceVM();
                    model.feedCurrentStockServiceVM.Date = DateTime.Now;
                    model.feedCurrentStockServiceVM.CurrentQuantity = 0;
                    model.feedCurrentStockServiceVM.CurrentUnitPrice = 0;
                }
                else
                {
                    model.feedCurrentStockServiceVM = new FeedCurrentStockServiceVM();
                    model.feedCurrentStockServiceVM.Date = stock.Date;
                    model.feedCurrentStockServiceVM.CurrentQuantity = stock.CurrentQuantity;
                    model.feedCurrentStockServiceVM.CurrentUnitPrice = stock.CurrentUnitPrice;
                }

                lists.Add(model);
            }

            return lists;
        }

        public async Task<FeedCategoryServiceVM> GetById(long id)
        {
            var feedCategory = await context.FeedCategories.FindAsync(id);
            FeedCategoryServiceVM model = new FeedCategoryServiceVM();
            if (feedCategory != null)
            {
                model.FeedCategoryName = feedCategory.FeedCategoryName;
                model.UnitName = feedCategory.UnitName;
                model.IsActive = feedCategory.IsActive;
                model.Id = id;
            }
            return model;
        }

        public async Task<FeedCategoryServiceVM> UpdateFeedCategory(FeedCategoryServiceVM model)
        {
            bool isExists = await context.FeedCategories.AnyAsync(c => c.FeedCategoryName == model.FeedCategoryName  && c.Id != model.Id);
            if (isExists)
            {
                model.ErrorMessage = "FeedCategory already exists. Please add unique category.";
                return model;
            }

            try
            {
                var feedCategory = await context.FeedCategories.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (feedCategory != null)
                {
                    feedCategory.FeedCategoryName = model.FeedCategoryName;
                    feedCategory.UnitName = model.UnitName;
                    feedCategory.UpdatedOn = DateTime.Now;
                    context.Entry(feedCategory).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new feedCategory. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> Remove(long id)
        {
            var feedCategory = await context.FeedCategories.FirstOrDefaultAsync(c => c.Id == id);
            feedCategory.IsActive = false;
            context.Entry(feedCategory).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
