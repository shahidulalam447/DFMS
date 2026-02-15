using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedConsumptionCowWise_Services
{
    public class FeedConsumptionCowWiseService : IFeedConsumptionCowWiseService
    {
        private readonly FirmDBContext context;

        public FeedConsumptionCowWiseService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<FeedConsumptionCowWiseServiceVM> AddNewFeedConsumptionCowWiseService(FeedConsumptionCowWiseServiceVM model)
        {
            try
            {
                if (model.FeedCategoryQuantities.Count > 0)
                {
                    foreach (var item in model.FeedCategoryQuantities)
                    {
                        var feedCurrentStock = await context.FeedCurrentStocks.FirstOrDefaultAsync(c => c.FeedCategoryId == item.Key);
                        if (feedCurrentStock != null)
                        {
                            if (item.Value <= feedCurrentStock.CurrentQuantity)
                            {
                                if (item.Value > 0)
                                {
                                    FeedConsumptionCowWise feedConsumptionCowWise = new FeedConsumptionCowWise();
                                    feedConsumptionCowWise.Date = model.Date;
                                    feedConsumptionCowWise.CowId = model.CowId;
                                    feedConsumptionCowWise.FeedCategoryId = item.Key;
                                    feedConsumptionCowWise.Quantity = item.Value;
                                    feedConsumptionCowWise.UnitPrice = feedCurrentStock.CurrentUnitPrice;
                                    feedConsumptionCowWise.CreatedOn = DateTime.Now;
                                    feedConsumptionCowWise.IsActive = true;
                                    context.FeedConsumptionCowWises.Add(feedConsumptionCowWise);
                                    var res = await context.SaveChangesAsync();
                                    if (res != 0)
                                    {
                                        model.Id = feedConsumptionCowWise.Id;
                                        feedCurrentStock.CurrentQuantity = feedCurrentStock.CurrentQuantity - item.Value;
                                        feedCurrentStock.UpdatedOn = DateTime.Now;
                                        context.Entry(feedCurrentStock).State = EntityState.Modified;
                                        await context.SaveChangesAsync();
                                    }
                                }
                               
                            }
                            else
                            {
                                model.ErrorMessage = "Consumption quantity can not exceed available quantity";
                            }

                        }
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

        public async Task<List<FeedConsumptionCowWiseServiceVM>> GetAll()
        {
            List<FeedConsumptionCowWiseServiceVM> lists = new List<FeedConsumptionCowWiseServiceVM>();
            var data = await context.FeedConsumptionCowWises.Where(x => x.IsActive).ToListAsync();
            foreach (var feedConsumptionCowWise in data)
            {
                FeedConsumptionCowWiseServiceVM model = new FeedConsumptionCowWiseServiceVM();
                model.Date = feedConsumptionCowWise.Date;
                model.CowId = feedConsumptionCowWise.CowId;
                model.CowTagNo = feedConsumptionCowWise.CowId == 0 ? "" : context.Cows.FirstOrDefault(a => a.Id == feedConsumptionCowWise.CowId).TagId;
                model.FeedCategoryId = feedConsumptionCowWise.FeedCategoryId;
                model.FeedCategoryName = feedConsumptionCowWise.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedConsumptionCowWise.FeedCategoryId).FeedCategoryName;
                model.Quantity = feedConsumptionCowWise.Quantity;
                model.UnitPrice = feedConsumptionCowWise.UnitPrice;
                model.IsActive = feedConsumptionCowWise.IsActive;
                model.Id = feedConsumptionCowWise.Id;
                lists.Add(model);
            }

            return lists;
        }

        public async Task<FeedConsumptionCowWiseServiceVM> GetById(long id)
        {
            var feedConsumptionCowWise = await context.FeedConsumptionCowWises.FindAsync(id);
            FeedConsumptionCowWiseServiceVM model = new FeedConsumptionCowWiseServiceVM();
            if (feedConsumptionCowWise != null)
            {
                model.Date = feedConsumptionCowWise.Date;
                model.CowId = feedConsumptionCowWise.CowId;
                model.CowTagNo = feedConsumptionCowWise.CowId == 0 ? "" : context.Cows.FirstOrDefault(a => a.Id == feedConsumptionCowWise.CowId).TagId;
                model.FeedCategoryId = feedConsumptionCowWise.FeedCategoryId;
                model.FeedCategoryName = feedConsumptionCowWise.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedConsumptionCowWise.FeedCategoryId).FeedCategoryName;
                model.Quantity = feedConsumptionCowWise.Quantity;
                model.UnitPrice = feedConsumptionCowWise.UnitPrice;
                model.IsActive = feedConsumptionCowWise.IsActive;
                model.Id = feedConsumptionCowWise.Id;
            }
            return model;
        }

        public async Task<FeedConsumptionCowWiseServiceVM> UpdateFeedConsumptionCowWiseService(FeedConsumptionCowWiseServiceVM model)
        {
            try
            {
                var feedConsumptionCowWise = await context.FeedConsumptionCowWises.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (feedConsumptionCowWise != null)
                {
                    feedConsumptionCowWise.Date = model.Date;
                    feedConsumptionCowWise.CowId = model.CowId;
                    feedConsumptionCowWise.FeedCategoryId = model.FeedCategoryId;
                    feedConsumptionCowWise.Quantity = model.Quantity;
                    feedConsumptionCowWise.UnitPrice = model.UnitPrice;
                    context.Entry(feedConsumptionCowWise).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new feedConsumptionCowWise. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> Remove(long id)
        {
            var feedConsumptionCowWise = await context.FeedConsumptionCowWises.FirstOrDefaultAsync(c => c.Id == id);
            feedConsumptionCowWise.IsActive = false;
            context.Entry(feedConsumptionCowWise).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
