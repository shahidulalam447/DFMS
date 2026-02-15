using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.FeedConsumptionBulk_Services
{
    public class FeedConsumptionBulkService  : IFeedConsumptionBulkService
    {
        private readonly FirmDBContext context;

        public FeedConsumptionBulkService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<FeedConsumptionBulkServiceVM> AddNewFeedConsumptionBulk(FeedConsumptionBulkServiceVM model)
        {
            try
            {
                if(model.feedConsumptionList is null)
                {
                    model.ErrorMessage= "Please Input Feed Quantity";
                    return model;
                }

              

                var feedCurrentStock = await context.FeedCurrentStocks.FirstOrDefaultAsync(c => c.FeedCategoryId == model.FeedCategoryId);
                if (feedCurrentStock != null )
                {

                    foreach(var data in model.feedConsumptionList)
                    {
                        model.Quantity+=data.Quantity;
                    }






                    if (feedCurrentStock.CurrentQuantity < model.Quantity)
                    {
                        model.ErrorMessage = "Invalid Quantity, amount unavailable in store";
                    }
                    else
                    {
                        FeedConsumptionBulk feedConsumptionBulk = new FeedConsumptionBulk();
                        feedConsumptionBulk.Date = model.Date;
                        feedConsumptionBulk.FeedCategoryId = model.FeedCategoryId;
                        feedConsumptionBulk.Quantity = model.Quantity;
                        feedConsumptionBulk.UnitPrice = /*model.UnitPrice;*/ feedCurrentStock.CurrentUnitPrice;
                        feedConsumptionBulk.CreatedOn = DateTime.Now;
                        feedConsumptionBulk.IsActive = true;
                        context.FeedConsumptionBulks.Add(feedConsumptionBulk);
                        var res = await context.SaveChangesAsync();

                        if (res != 0)
                        {
                            model.Id = feedConsumptionBulk.Id;
                            //update stock table
                            feedCurrentStock.CurrentQuantity = feedCurrentStock.CurrentQuantity - feedConsumptionBulk.Quantity;
                            feedCurrentStock.UpdatedOn = DateTime.Now;
                            context.Entry(feedCurrentStock).State = EntityState.Modified;
                            await context.SaveChangesAsync();

                            //cow wise consumption                                           

                            List<FeedConsumptionCowWise> CowWiseList = new List<FeedConsumptionCowWise>();
                            foreach (var liveStock in model.feedConsumptionList)
                            {

                                var cowList = await context.Cows.AsQueryable().AsNoTracking().Where(c => c.IsActive==true &&c.ShedNo.Equals(liveStock.ShadeNo.ToString())
                                                                   && c.LineNo.Equals(liveStock.LineNo.ToString())).ToListAsync();

                                decimal UnitPerLiveStock = liveStock.Quantity/ cowList.Count();

                                foreach (var cow in cowList)
                                {
                                    FeedConsumptionCowWise feedConsumptionCowWise = new FeedConsumptionCowWise();
                                    feedConsumptionCowWise.Date = model.Date; ;
                                    feedConsumptionCowWise.CowId = cow.Id;
                                    feedConsumptionCowWise.FeedCategoryId = feedConsumptionBulk.FeedCategoryId;
                                    feedConsumptionCowWise.Quantity = UnitPerLiveStock;
                                    feedConsumptionCowWise.UnitPrice = feedConsumptionBulk.UnitPrice;
                                    feedConsumptionCowWise.CreatedOn = feedConsumptionBulk.CreatedOn;
                                    feedConsumptionCowWise.IsActive = true;
                                    CowWiseList.Add(feedConsumptionCowWise);
                                }

                               
                            }
                            try
                            {
                                await context.AddRangeAsync(CowWiseList);
                                await context.SaveChangesAsync();
                            }
                            catch(Exception ex)
                            {
                                model.ErrorMessage = "Error  "+ex.Message;
                                return model;
                            }

                        }
                    }
                    
                }
                else
                {
                    model.ErrorMessage = "Invalid Category, store is empty";
                }
                


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new feed entry. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<FeedConsumptionBulkServiceVM>> GetAll()
        {
            List<FeedConsumptionBulkServiceVM> lists = new List<FeedConsumptionBulkServiceVM>();
            var data = await context.FeedConsumptionBulks.Where(x => x.IsActive && x.Date.Date>=DateTime.Now.Date.AddDays(-3)).ToListAsync();
            foreach (var feedConsumptionBulk in data)
            {
                FeedConsumptionBulkServiceVM model = new FeedConsumptionBulkServiceVM();
                model.Date = feedConsumptionBulk.Date;
                model.FeedCategoryId = feedConsumptionBulk.FeedCategoryId;
                model.FeedCategoryName = feedConsumptionBulk.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedConsumptionBulk.FeedCategoryId).FeedCategoryName;
                model.Quantity = feedConsumptionBulk.Quantity;
                model.UnitPrice = feedConsumptionBulk.UnitPrice;
                model.IsActive = feedConsumptionBulk.IsActive;
                model.Id = feedConsumptionBulk.Id;
                lists.Add(model);
            }

            return lists;
        }

        public async Task<FeedConsumptionBulkServiceVM> GetById(long id)
        {
            var feedConsumptionBulk = await context.FeedConsumptionBulks.FindAsync(id);
            FeedConsumptionBulkServiceVM model = new FeedConsumptionBulkServiceVM();
            if (feedConsumptionBulk != null)
            {
                model.Date = feedConsumptionBulk.Date;
                model.FeedCategoryId = feedConsumptionBulk.FeedCategoryId;
                model.FeedCategoryName = feedConsumptionBulk.FeedCategoryId == 0 ? "" : context.FeedCategories.FirstOrDefault(a => a.Id == feedConsumptionBulk.FeedCategoryId).FeedCategoryName;
                model.Quantity = feedConsumptionBulk.Quantity;
                model.UnitPrice = feedConsumptionBulk.UnitPrice;
                model.IsActive = feedConsumptionBulk.IsActive;
                model.Id = feedConsumptionBulk.Id;
            }
            return model;
        }

        public async Task<FeedConsumptionBulkServiceVM> UpdateFeedConsumptionBulk(FeedConsumptionBulkServiceVM model)
        {
            try
            {
                var feedConsumptionBulk = await context.FeedConsumptionBulks.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (feedConsumptionBulk != null)
                {
                    feedConsumptionBulk.Date = model.Date;
                    feedConsumptionBulk.FeedCategoryId = model.FeedCategoryId;
                    feedConsumptionBulk.Quantity = model.Quantity;
                    feedConsumptionBulk.UnitPrice = model.UnitPrice;
                    //context.FeedConsumptionBulks.Add(feedConsumptionBulk);
                    //await context.SaveChangesAsync();
                    context.Entry(feedConsumptionBulk).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new feedConsumptionBulk. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> Remove(long id)
        {
            var feedConsumptionBulk = await context.FeedConsumptionBulks.FirstOrDefaultAsync(c => c.Id == id);
            feedConsumptionBulk.IsActive = false;
            context.Entry(feedConsumptionBulk).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FeedConsumptionBulkServiceVM>> ShadeLineFeedList()
        {
            var shadeList =await  context.Cows.AsQueryable().AsNoTracking()
                         .Where(c => c.IsActive == true).OrderBy(c => c.ShedNo)
                         .Select(c => new { shade = c.ShedNo, Line = c.LineNo, }).Distinct()
                         .OrderBy(c=>c.Line).ToListAsync();

           var modelList=new List<FeedConsumptionBulkServiceVM>();
            foreach(var shade in shadeList)
            {
                var model= new FeedConsumptionBulkServiceVM()
                {
                    ShadeNo=Convert.ToInt16(shade.shade),
                    LineNo=Convert.ToInt16(shade.Line)

                };


                modelList.Add(model);
            }

            return modelList;
        }
    }
}
