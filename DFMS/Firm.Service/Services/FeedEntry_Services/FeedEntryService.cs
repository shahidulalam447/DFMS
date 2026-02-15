using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Firm.Service.Services.FeedCategory_Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedEntry_Services
{
    public class FeedEntryService : IFeedEntryService
    {
        private readonly FirmDBContext context;

        public FeedEntryService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<FeedEntryServiceVM> AddNewFeedEntry(FeedEntryServiceVM model)
        {
            try
            {
                FeedEntry feedEntry = new FeedEntry();
                feedEntry.Date = model.Date;
                feedEntry.FeedCategoryId = model.FeedCategoryId;
                feedEntry.Quantity = model.Quantity;
                feedEntry.UnitPrice = model.UnitPrice;
                feedEntry.CreatedOn = DateTime.Now;
                feedEntry.IsActive = true;
                context.FeedEntries.Add(feedEntry);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.Id = feedEntry.Id;
                    // Check if the category exists in the stock
                    bool isExists = await context.FeedCurrentStocks.AnyAsync(c => c.FeedCategoryId == model.FeedCategoryId);

                    if (isExists)
                    {
                        var feedCurrentStock = await context.FeedCurrentStocks.FirstOrDefaultAsync(c => c.FeedCategoryId == model.FeedCategoryId);

                        // Update the current quantity and unit price
                      
                        feedCurrentStock.CurrentUnitPrice = (feedCurrentStock.CurrentUnitPrice * feedCurrentStock.CurrentQuantity + feedEntry.UnitPrice * feedEntry.Quantity) / (feedCurrentStock.CurrentQuantity + feedEntry.Quantity);
                        feedCurrentStock.CurrentQuantity += feedEntry.Quantity;
                        feedCurrentStock.UpdatedOn = feedEntry.CreatedOn;

                        context.Entry(feedCurrentStock).State = EntityState.Modified;
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        // Create a new entry in the stock for the category
                        FeedCurrentStock feedCurrentStock = new FeedCurrentStock();
                        feedCurrentStock.FeedCategoryId = feedEntry.FeedCategoryId;
                        feedCurrentStock.CurrentQuantity = feedEntry.Quantity;
                        feedCurrentStock.CurrentUnitPrice = feedEntry.UnitPrice;
                        feedCurrentStock.CreatedOn = feedEntry.CreatedOn;
                        feedCurrentStock.IsActive = true;

                        context.FeedCurrentStocks.Add(feedCurrentStock);
                        await context.SaveChangesAsync();
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new feed entry. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<FeedEntryServiceVM>> GetAll()
        {
            List<FeedEntryServiceVM> lists = new List<FeedEntryServiceVM>();
            var data = await context.FeedEntries.Where(x => x.IsActive && x.Date.Date>=DateTime.Now.Date.AddDays(-7)).ToListAsync();
            foreach (var feedEntry in data)
            {
                FeedEntryServiceVM model = new FeedEntryServiceVM();
                model.Date = feedEntry.Date;
                model.FeedCategoryId = feedEntry.FeedCategoryId;
                model.FeedCategoryName = feedEntry.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedEntry.FeedCategoryId).FeedCategoryName;
                model.Quantity = feedEntry.Quantity;
                model.UnitPrice = feedEntry.UnitPrice;
                model.IsActive = feedEntry.IsActive;
                model.Id = feedEntry.Id;
                lists.Add(model);
            }

            return lists;
        }

        public async Task<FeedEntryServiceVM> GetById(long id)
        {
            var feedEntry = await context.FeedEntries.FindAsync(id);
            FeedEntryServiceVM model = new FeedEntryServiceVM();
            if (feedEntry != null)
            {
                model.Date = feedEntry.Date;
                model.FeedCategoryId = feedEntry.FeedCategoryId;
                model.FeedCategoryName = feedEntry.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedEntry.FeedCategoryId).FeedCategoryName;
                model.Quantity = feedEntry.Quantity;
                model.UnitPrice = feedEntry.UnitPrice;
                model.IsActive = feedEntry.IsActive;
                model.Id = feedEntry.Id;
            }
            return model;
        }

        public async Task<FeedEntryServiceVM> UpdateFeedEntry(FeedEntryServiceVM model)
        {
            try
            {
                var feedEntry = await context.FeedEntries.FirstOrDefaultAsync(c => c.Id == model.Id);

                if (feedEntry != null)
                {
                    decimal originalQuantity = feedEntry.Quantity;
                    decimal originalUnitPrice = feedEntry.UnitPrice;

                    feedEntry.Date = model.Date;
                    feedEntry.FeedCategoryId = model.FeedCategoryId;
                    feedEntry.Quantity = model.Quantity;
                    feedEntry.UnitPrice = model.UnitPrice;
                    feedEntry.UpdatedOn = DateTime.Now;

                    context.Entry(feedEntry).State = EntityState.Modified;
                    var res = await context.SaveChangesAsync();

                    if (res != 0)
                    {
                        model.Id = feedEntry.Id;

                        // Check if the category exists in the stock
                        bool isExists = await context.FeedCurrentStocks.AnyAsync(c => c.FeedCategoryId == model.FeedCategoryId);

                        if (isExists)
                        {
                            var feedCurrentStock = await context.FeedCurrentStocks.FirstOrDefaultAsync(c => c.FeedCategoryId == model.FeedCategoryId);
                            decimal excurentquantitystock = feedCurrentStock.CurrentQuantity;
                            feedCurrentStock.CurrentQuantity = (feedCurrentStock.CurrentQuantity - originalQuantity) + model.Quantity;

                            // Update the current quantity and unit price
                            feedCurrentStock.CurrentUnitPrice = ((feedCurrentStock.CurrentUnitPrice * excurentquantitystock) - (originalUnitPrice * originalQuantity) + (feedEntry.UnitPrice * model.Quantity)) / (feedCurrentStock.CurrentQuantity);


                            feedCurrentStock.UpdatedOn = feedEntry.CreatedOn;

                            context.Entry(feedCurrentStock).State = EntityState.Modified;
                            await context.SaveChangesAsync();
                        }
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new feedEntry. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> Remove(long id)
        {
            var feedEntry = await context.FeedEntries.FirstOrDefaultAsync(c => c.Id == id);
            feedEntry.IsActive = false;
            context.Entry(feedEntry).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
