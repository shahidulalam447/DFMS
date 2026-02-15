using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.SaleableItem_Services
{
    public class SaleableItemService : ISaleableItemService
    {
        private readonly FirmDBContext context;

        public SaleableItemService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<SaleableItemServiceVM> AddNewSaleableItem(SaleableItemServiceVM model)
        {
            bool isExists = await context.SaleableItems.AnyAsync(c => c.ItemName == model.ItemName);
            if (isExists)
            {
                model.ErrorMessage = "This Item already exists. Please add a unique item.";
                return model;
            }
            try
            {
                SaleableItem saleableItem = new SaleableItem();
                saleableItem.ItemName = model.ItemName;
                saleableItem.UnitName = model.UnitName;
                saleableItem.CreatedOn = DateTime.Now;
                saleableItem.IsActive = true;
                context.SaleableItems.Add(saleableItem);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.Id = saleableItem.Id;
                }


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new item. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<SaleableItemServiceVM>> GetAll()
        {
            List<SaleableItemServiceVM> lists = new List<SaleableItemServiceVM>();
            var data = await context.SaleableItems.Where(x => x.IsActive).ToListAsync();
            foreach (var saleableItem in data)
            {
                SaleableItemServiceVM model = new SaleableItemServiceVM();
                model.ItemName = saleableItem.ItemName;
                model.UnitName = saleableItem.UnitName;
                model.IsActive = saleableItem.IsActive;
                model.Id = saleableItem.Id;
                lists.Add(model);
            }

            return lists;
        }

       
        public async Task<SaleableItemServiceVM> GetById(long id)
        {
            var saleableItem = await context.SaleableItems.FindAsync(id);
            SaleableItemServiceVM model = new SaleableItemServiceVM();
            if (saleableItem != null)
            {
                model.ItemName = saleableItem.ItemName;
                model.UnitName = saleableItem.UnitName;
                model.IsActive = saleableItem.IsActive;
                model.Id = id;
            }
            return model;
        }

        public async Task<SaleableItemServiceVM> UpdateSaleableItem(SaleableItemServiceVM model)
        {
            bool isExists = await context.SaleableItems.AnyAsync(c => c.ItemName == model.ItemName && c.Id != model.Id);
            if (isExists)
            {
                model.ErrorMessage = "SaleableItem already exists. Please add unique item.";
                return model;
            }

            try
            {
                var saleableItem = await context.SaleableItems.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (saleableItem != null)
                {
                    saleableItem.ItemName = model.ItemName;
                    saleableItem.UnitName = model.UnitName;
                    saleableItem.UpdatedOn = DateTime.Now;
                    //context.SaleableItems.Add(saleableItem);
                    //await context.SaveChangesAsync();
                    context.Entry(saleableItem).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new saleable Item. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> Remove(long id)
        {
            var saleableItem = await context.SaleableItems.FirstOrDefaultAsync(c => c.Id == id);
            saleableItem.IsActive = false;
            context.Entry(saleableItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
