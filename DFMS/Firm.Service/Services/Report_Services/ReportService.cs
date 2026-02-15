using Firm.Infrastructure.Data;
using Firm.Service.Services.Report_Services.ReportViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.Report_Services
{
    public class ReportService : IReportService
    {
        private readonly FirmDBContext _context;

        public ReportService(FirmDBContext context)
        {

            _context = context;
        }




        public async Task<Vaccine_Treatment_ReportVM> Treatment_Report(Vaccine_Treatment_ReportVM VTReportVM)
        {
            var tratmentData = await _context.Treatments.AsQueryable().AsNoTracking()
                            .Where(c => c.IsActive == true && (c.TreatmentDate >= VTReportVM.StartDate &&
                            c.TreatmentDate <= VTReportVM.EndDate)).OrderBy(c => c.TreatmentDate)
                            .Select(c => new { c.TreatmentDate, c.CowId, c.DoctorId, c.Investigation, c.Price })
                            .ToListAsync();


            var vaccineData = await _context.Vaccines.AsQueryable().AsNoTracking()
                            .Where(c => c.IsActive == true && (c.VaccineDate >= VTReportVM.StartDate &&
                            c.VaccineDate <= VTReportVM.EndDate)).OrderBy(c => c.VaccineDate)
                            .Select(c => new { c.VaccineDate, c.CowId, c.DoctorId, c.Name, c.Price })
                            .ToListAsync();


            var vtModelList = new List<Vaccine_Treatment_ReportVM>();

            foreach (var data in tratmentData)
            {
                var Doctor = await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(c => c.Id == data.DoctorId);
                var cow = await _context.Cows.AsNoTracking().FirstOrDefaultAsync(c => c.Id == data.CowId);
                var model = new Vaccine_Treatment_ReportVM()
                {
                    Day = data.TreatmentDate.ToString("dd MMM yy"),
                    TagId = cow.TagId,
                    TreatmentFor = data.Investigation,
                    DoctorName = Doctor.DoctorName,
                    Price = data.Price ?? 0,
                    CostingType= "Treatment"
                };

                vtModelList.Add(model);



            }
            foreach (var data in vaccineData)
            {
                var Doctor = await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(c => c.Id == data.DoctorId);
                var cow = await _context.Cows.AsNoTracking().FirstOrDefaultAsync(c => c.Id == data.CowId);
                var model = new Vaccine_Treatment_ReportVM()
                {
                    Day = data.VaccineDate.ToString("dd MMM yy"),
                    TagId = cow.TagId,
                    TreatmentFor = data.Name,
                    DoctorName = Doctor.DoctorName,
                    CostingType = "Vaccine",
                    Price = data.Price
                };

                vtModelList.Add(model);



            }
            var trObject = new Vaccine_Treatment_ReportVM();
            trObject.StartDate = VTReportVM.StartDate;
            trObject.EndDate = VTReportVM.EndDate;
            trObject.VTReportVM = vtModelList;
            trObject.TottalDay = vtModelList.DistinctBy(c => c.Day).Count();
            trObject.TottalCow = vtModelList.DistinctBy(c => c.TagId).Count();
            trObject.TottalPrice = vtModelList.Sum(c => c.Price);
            trObject.VTReportVM.OrderBy(c => c.Day).ToList();

            return trObject;
        }
        public async Task<FeddingCostReportVM> FeddingCostReport(FeddingCostReportVM FeddingReport)
        {
            var feedingData = await  _context.FeedConsumptionCowWises.AsQueryable().AsNoTracking()
                 .Where(c => c.IsActive == true & (c.Date >= FeddingReport.StartDate && c.Date <= FeddingReport.EndDate))
                 .OrderBy(c => c.Date).GroupBy(c=>c.Date)
                 .Select(c => new { date = c.Key,feedData= c.GroupBy(x=>x.CowId)
                 .Select(x=>new {cowId=x.Key,foodUnit= x.Sum(c=>c.Quantity),Price=x.Sum(x=>x.UnitPrice*x.Quantity)})})
                 .ToListAsync();
               
           var feedCostList= new List<FeddingCostReportVM>();

            foreach (var feed in feedingData)
            {

                foreach (var data in feed.feedData)
                {
                    var cow = await _context.Cows.AsNoTracking().FirstOrDefaultAsync(c => c.Id == data.cowId);
                    var feedObject = new FeddingCostReportVM()
                    {
                        Day = feed.date.ToString("dd MMM yy"),
                        TagNo = cow.TagId,
                        Consumption = data.Price,
                        FoodUnit= data.foodUnit

                    };
                    feedCostList.Add(feedObject);
                }
            }
            var feedingCostReport = new FeddingCostReportVM();
            feedingCostReport.FeddingCostList= feedCostList;
            feedingCostReport.StartDate= FeddingReport.StartDate;
            feedingCostReport.EndDate= FeddingReport.EndDate;
            feedingCostReport.TottalDay= feedCostList.DistinctBy(c=>c.Day).Count();
            feedingCostReport.TottalCow = feedCostList.DistinctBy(c => c.TagNo).Count();
            feedingCostReport.TottalConsumption= feedCostList.Sum(c=>c.Consumption);
            feedingCostReport.TottalFoodUnit= feedCostList.Sum(c=>c.FoodUnit);
            feedingCostReport.FeddingCostList.OrderBy(c => c.Day).ToList();

            return feedingCostReport;
        }



        public async Task<MilkReportVM> MilkReport(MilkReportVM milkReport)
        {

            var milkData = await _context.MilkMonitors.AsQueryable().AsNoTracking()
                .Where(c => c.IsActive == true && (c.Date >= milkReport.StartDate && c.Date <= milkReport.EndDate))
                .OrderBy(c => c.Date)
                .GroupBy(c => c.Date)
                .Select(DateData => new
                {
                    date = DateData.Key,
                    cowData = DateData
                    .GroupBy(c => c.CowId)
                    .Select(cow => new { cowId = cow.Key, tottalMilk = cow.Sum(c => c.TotalMilk) })
                }).ToListAsync();

            var model = new List<MilkReportVM>();
            foreach (var milk in milkData)
            {
                foreach (var cow in milk.cowData)
                {


                    if (cow is null | cow.tottalMilk == (0 | null))
                    {
                        continue;
                    }
                    var MilkReport = new MilkReportVM();
                    MilkReport.Day = milk.date.ToString("dd MMM yy");
                    MilkReport.CowTagId = await _context.Cows.AsNoTracking().Where(c => c.Id == cow.cowId).Select(c => c.TagId).FirstOrDefaultAsync();
                    MilkReport.TotalMilk = cow.tottalMilk;


                    model.Add(MilkReport);
                }


            }
            var milkReportObject = new MilkReportVM();
            milkReportObject.StartDate = milkReport.StartDate;
            milkReportObject.EndDate = milkReport.EndDate;
            milkReportObject.ListMilkReportVM = model;
            milkReportObject.TotalCowMilk = milkReportObject.ListMilkReportVM.Sum(c => c.TotalMilk);
            milkReportObject.TotalCow = model.DistinctBy(c => c.CowTagId).Count();
            milkReportObject.tottalDay = model.DistinctBy(c => c.Day).Count();
            return milkReportObject;
        }
        public async Task<IndividualCowReportVM> IndividualCowSummary(IndividualCowReportVM individualCow)
        {
            var TotalCost = await _context.Cows.AsQueryable().AsNoTracking()
               .Where(c => c.IsActive == true && c.TagId.Equals(individualCow.TagId.ToString())).Select(c => new
               {
                   tagId = c.TagId,
                   cowId= c.Id,
                   cowBuy = c.Price,
                   firstWeight = c.Weight,
                   liveStockType=c.LivestockTypeVal,
                   vaccineCost = _context.Vaccines.AsNoTracking().Where(x => x.IsActive == true && x.CowId == c.Id)
                   .Sum(v => v.Price),
                   tratmentCost = _context.Treatments.AsNoTracking().Where(x => x.IsActive == true && x.CowId == c.Id)
                   .Sum(v => v.Price),
                   feedingCost = _context.FeedConsumptionCowWises.AsNoTracking().Where(x => x.IsActive == true && x.CowId == c.Id)
                   .Sum(c => c.Quantity * c.UnitPrice),
               }).ToListAsync();

          


            var individualCowCost = new IndividualCowReportVM();
            foreach (var cow in TotalCost)
            {
                decimal totalMilk = 0;
                if (cow.liveStockType== LivestockType.Cow)
                {
                     totalMilk = _context.MilkMonitors.AsQueryable().AsNoTracking()
                                       .Where(c => c.CowId == cow.cowId && c.IsActive == true).Select(c => c.TotalMilk)
                                       .ToList().Sum();
                }
                individualCowCost.LivestockType = (LivestockType)cow.liveStockType;
                individualCowCost.TotalMilkEarn = totalMilk ;
                individualCowCost.BuyCost = cow.cowBuy;
                individualCowCost.TotalVacCost = cow.vaccineCost;
                individualCowCost.TotalTreatment = cow.tratmentCost;
                individualCowCost.TotalFeedCost = cow.feedingCost;
                individualCowCost.Weight = individualCow.Weight;
                individualCowCost.CurrentMeatPrice = individualCow.CurrentMeatPrice;
                individualCowCost.CowPrice = (individualCow.Weight + Convert.ToDecimal(cow.firstWeight)) * individualCow.CurrentMeatPrice;
                individualCowCost.TotalCowCost = Convert.ToDecimal(cow.cowBuy) + cow.vaccineCost + cow.tratmentCost + cow.feedingCost;

            }

            return individualCowCost;
        }
        public async Task<CowCostTotalVM> CowCost()
        {

            var TotalCost = await _context.Cows.AsQueryable().AsNoTracking()
                           .Where(c => c.IsActive == true).Select(c => new
                           {
                               tagId = c.TagId,
                               cowBuy = c.Price,
                               vaccineCost = _context.Vaccines.AsNoTracking().Where(x => x.IsActive == true && x.CowId == c.Id)
                               .Sum(v => v.Price),
                               tratmentCost = _context.Treatments.AsNoTracking().Where(x => x.IsActive == true && x.CowId == c.Id)
                               .Sum(v => v.Price),
                               feedingCost = _context.FeedConsumptionCowWises.AsNoTracking().Where(x => x.IsActive == true && x.CowId == c.Id)
                               .Sum(c => c.Quantity * c.UnitPrice),
                           }).ToListAsync();


            var CowCostList = new List<CowCostTotalVM>();
            foreach (var cost in TotalCost)
            {
                var CowCost = new CowCostTotalVM()
                {
                    TagNo = cost.tagId,
                    VacCost = cost.vaccineCost,
                    FeedCost = cost.feedingCost,
                    Treatment = cost.tratmentCost,
                    BuyCost = cost.cowBuy,
                    perCowCosting = Convert.ToDecimal(Convert.ToDecimal(cost.vaccineCost)
                                   + Convert.ToDecimal(cost.feedingCost) +
                                   Convert.ToDecimal(cost.tratmentCost) +
                                    Convert.ToDecimal(cost.cowBuy))

                };

                CowCostList.Add(CowCost);
            }
            var cowcost = new CowCostTotalVM();
            cowcost.CowCostList = CowCostList;
            cowcost.TotalVacCost = CowCostList.Sum(c => c.VacCost);
            cowcost.TotalTreatment = CowCostList.Sum(c => c.Treatment);
            cowcost.TotalFeedCost = CowCostList.Sum(c => c.FeedCost);
            cowcost.Cow = CowCostList.DistinctBy(c => c.TagNo).Count();
            cowcost.TotalCowCost = CowCostList.Sum(c => c.perCowCosting);
            cowcost.CowBuying = Convert.ToDecimal(CowCostList.Sum(c => c.BuyCost));

            return cowcost;
        }

    }
}
