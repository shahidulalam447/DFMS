using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.Milk_Services
{
    public class MilkService : IMilkService
    {
        private readonly FirmDBContext context;

        public MilkService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<MilkServiceViewModel> AddNewMilk(MilkServiceViewModel model)
        {

            try
            {
                var milkList = new List<MilkMonitor>();

                foreach (var milkmodel in model.milkServiceVmList)
                {
                    MilkMonitor milk = new MilkMonitor();
                    milk.Date = model.Date;
                    milk.ShiftVal = model.ShiftVal ;
                    milk.TotalMilk = milkmodel.TotalMilk;
                    milk.CowId = milkmodel.CowId;
                    milk.CreatedOn = DateTime.Now;
                    milk.IsActive = true;
                    milkList.Add(milk);
                }
                context.MilkMonitors.AddRange(milkList);
                var res = await context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new milk. Details: " + ex.Message;
                return model;
            }
        }

        //public async Task<List<MilkServiceViewModel>> GetAll()
        //{
        //    var fromDate = DateTime.Today.AddDays(-1);

        //    var data = await context.MilkMonitors
        //        .AsNoTracking()
        //        .Where(x => x.IsActive && x.Date >= fromDate)
        //        .GroupBy(x => new { x.Date, x.CowId })
        //        .Select(g => new
        //        {
        //            g.Key.Date,
        //            g.Key.CowId,
        //            Id = g.Select(x => x.Id).FirstOrDefault(),
        //            DayShift = g.Where(x => x.ShiftVal == (Shift)2).Sum(x => x.TotalMilk),
        //            MourningShift = g.Where(x => x.ShiftVal == (Shift)1).Sum(x => x.TotalMilk),
        //            EveningShift = g.Where(x => x.ShiftVal == (Shift)3).Sum(x => x.TotalMilk),
        //            Remark = g.Select(x => x.Remarks).FirstOrDefault()
        //        })
        //        .ToListAsync();

        //    // 🔥 Get all cows in one query
        //    var cowIds = data.Select(x => x.CowId).Distinct().ToList();

        //    var cows = await context.Cows
        //        .AsNoTracking()
        //        .Where(c => cowIds.Contains(c.Id))
        //        .ToDictionaryAsync(c => c.Id);

        //    var result = data.Select(x =>
        //    {
        //        cows.TryGetValue(x.CowId, out var cow);

        //        return new MilkServiceViewModel
        //        {
        //            Date = x.Date,
        //            CowId = x.CowId,
        //            Id = x.Id,
        //            DayShift = x.DayShift,
        //            MourningShift = x.MourningShift,
        //            EveningShift = x.EveningShift,
        //            TotalMilk = x.DayShift + x.MourningShift + x.EveningShift,
        //            Remarks = x.Remark,
        //            CowTagId = cow?.TagId ?? ""
        //        };
        //    }).ToList();

        //    return result;
        //}


        public async Task<List<MilkServiceViewModel>> GetAll()
        {
            List<MilkServiceViewModel> lists = new List<MilkServiceViewModel>();
            var data = await context.MilkMonitors.AsQueryable().AsNoTracking().Where(x => x.IsActive /* == true && x.Date.Date >= DateTime.Now.Date.AddDays(-1)*/)
                .GroupBy(c => c.Date).Select(c => new
                {
                    date = c.Key,
                    milkdata = c.GroupBy(c => c.CowId).Select(c => new
                    {
                        cowId = c.Key,
                        id = c.Select(c => c.Id).FirstOrDefault(),
                        dayShift = c.Where(c => c.ShiftVal == (Shift)2).Select(c => c.TotalMilk).ToList().AsQueryable().Sum(),
                        mourningShift = c.Where(c => c.ShiftVal == (Shift)1).Select(c => c.TotalMilk).ToList().AsQueryable().Sum(),
                        eveningShift = c.Where(c => c.ShiftVal == (Shift)3).Select(c => c.TotalMilk).ToList().AsQueryable().Sum(),
                        remark = c.Select(c => c.Remarks).FirstOrDefault()
                    }),
                }).ToListAsync();
            foreach (var milk in data)
            {
                foreach (var cow in milk.milkdata)
                {
                    var cowObject = context.Cows.AsNoTracking().FirstOrDefault(a => a.Id == cow.cowId);
                    MilkServiceViewModel model = new MilkServiceViewModel();
                    model.Date = milk.date;
                    model.DayShift = cow.dayShift;
                    model.MourningShift = cow.mourningShift;
                    model.EveningShift = cow.eveningShift;
                    model.TotalMilk = cow.dayShift + cow.mourningShift + cow.eveningShift;
                    model.Remarks = cow.remark;
                    model.CowId = cowObject.Id;
                    model.Id = cow.id;
                    model.CowTagId = cow.cowId == 0 ? "" : cowObject.TagId;

                    lists.Add(model);
                }
            }
            return lists;
        }

        public async Task<MilkServiceViewModel> GetById(long id)
        {
            var milk = await context.MilkMonitors.FindAsync(id);

            var milkdata = await context.MilkMonitors.AsQueryable().AsNoTracking()
                       .Where(c => c.CowId == milk.CowId && c.Date == milk.Date&& c.IsActive==true).GroupBy(c => c.Date)
                       .Select(c => new
                       {
                           date = c.Select(c => c.Date).FirstOrDefault(),
                           dayShift = c.Where(c => c.ShiftVal == (Shift)2).Select(c => c.TotalMilk).ToList().AsQueryable().Sum(),
                           mourningShift = c.Where(c => c.ShiftVal == (Shift)1).Select(c => c.TotalMilk).ToList().AsQueryable().Sum(),
                           eveningShift = c.Where(c => c.ShiftVal == (Shift)3).Select(c => c.TotalMilk).ToList().AsQueryable().Sum(),
                           remark = c.Select(c => c.Remarks).FirstOrDefault()
                       }).ToListAsync();


            MilkServiceViewModel model = new MilkServiceViewModel();
            foreach (var cowMilk in milkdata)
            {

                model.Date = cowMilk.date;
                model.DayShift = cowMilk.dayShift;
                model.MourningShift = cowMilk.mourningShift;
                model.EveningShift = cowMilk.eveningShift;
                model.TotalMilk = cowMilk.dayShift + cowMilk.mourningShift + cowMilk.eveningShift;
                model.Remarks = cowMilk.remark;
                model.CowId = milk.CowId;
                model.Id = milk.Id;
                model.CowTagId = milk.CowId == 0 ? "" : context.Cows.AsNoTracking().FirstOrDefault(a => a.Id == milk.CowId).TagId;

            }



            return model;
        }
        public async Task<MilkServiceViewModel> UpdateMilk(MilkServiceViewModel model)
        {
            var milkData = await context.MilkMonitors.AsQueryable().AsNoTracking()
                         .Where(c => c.CowId == model.CowId && c.Date == model.Date &&c.IsActive==true)
                         .ToListAsync();
          
            int dayShift = 0;
            int mourningShift = 0;
            int eveningShift = 0;
            
            foreach (var milk in milkData)
            {
               

                if (milk.ShiftVal == (Shift)2 && dayShift<1)
                {
                    milk.TotalMilk = Convert.ToDecimal(model.DayShift);

                  
                    dayShift++;
                   
                }else if (milk.ShiftVal == (Shift)1 && mourningShift < 1)
                {
                    milk.TotalMilk = Convert.ToDecimal(model.MourningShift);
                
                    mourningShift++;
                    
                }else if (milk.ShiftVal == (Shift)3 && eveningShift < 1)
                {
                    milk.TotalMilk = Convert.ToDecimal(model.EveningShift);
                  
                    eveningShift++;
                    
                }else{
                        milk.TotalMilk = 0;

                        
                }
                    
                

                milk.UpdatedOn = DateTime.Now;
                milk.Remarks = model.Remarks;


            }

            try
            {

                context.UpdateRange(milkData);
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new milk. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<bool> Remove(long id)
        {
            var milk = await context.MilkMonitors.FindAsync(id);
            var milkList = await context.MilkMonitors.AsQueryable()
                           .Where(x => x.CowId == milk.CowId && x.Date == milk.Date)
                           .ToListAsync();

            foreach (var data in milkList)
            {
                data.IsActive = false;
            }

            try
            {
                context.UpdateRange(milkList);
                await context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }

        }

        public async Task<List<MilkServiceViewModel>> MilkingCowList(MilkServiceViewModel model)
        {
            if (model is null)
            {

            }
            var cowList = await context.Cows.AsQueryable().AsNoTracking()

                .Where(c => c.IsActive == true && c.LivestockTypeVal == (LivestockType)2
                && c.ShedNo.Equals(model.ShadeNo))

                .Select(c => new { cowId = c.Id, tagId = c.TagId }).OrderBy(c => c.tagId).ToListAsync();


            var listVM = new List<MilkServiceViewModel>();
            foreach (var cow in cowList)
            {
                var modelObject = new MilkServiceViewModel()
                {
                    CowId = cow.cowId,
                    CowTagId = cow.tagId,
                    ShiftVal=model.ShiftVal,
                    Date=model.Date
                    
                };



                listVM.Add(modelObject);

            }
            return listVM;
        }
    }
}
