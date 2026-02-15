
using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Firm.Service.Services.FeedConsumptionCowWise_Services;
using Firm.Service.Services.Milk_Services;
using Firm.Service.Services.Treatment_Services;
using Firm.Service.Services.Vaccine_Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.Cow_Services
{
    public class CowService : ICowService
    {
        private readonly FirmDBContext context;

        public CowService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<CowServiceViewModel> AddNewCow(CowServiceViewModel model)
        {
            bool isTagIdExists = await context.Cows.AnyAsync(c => c.TagId == model.TagId);
            if (isTagIdExists)
            {
                model.ErrorMessage = "TagId already exists. Please choose a unique TagId.";
                return model;
            }
            try
            {
                Cow cow = new Cow();
                cow.PurchaseDate = model.PurchaseDate;
                cow.CalfGender = model.CalfGender;
                cow.Age = model.Age;
                cow.TagId = model.TagId;
                cow.BreedId = model.BreedId;
                cow.IsActive = true;
                cow.Price = model.Price;
                cow.MotherTag = model.MotherTag;
                cow.Origin = model.Origin;
                cow.Weight = model.Weight;
                cow.CowTeeth = model.CowTeeth;
                cow.Color = model.Color;
                cow.ShedNo = model.ShedNo;
                cow.LineNo = model.LineNo;
                cow.LivestockTypeVal = model.LivestockTypeVal;
                cow.CreatedOn = DateTime.Now;
                context.Cows.Add(cow);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.Id = cow.Id;
                }


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new cow. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<CowServiceViewModel>> GetAll()
        {
            List<CowServiceViewModel> lists = new List<CowServiceViewModel>();
            var data = await context.Cows.Where(x => x.IsActive).ToListAsync();

            foreach (var cow in data)
            {
                CowServiceViewModel model = new CowServiceViewModel();
                model.PurchaseDate = cow.PurchaseDate;
                model.Price = cow.Price;
                model.Id = cow.Id;
                model.Age = cow.Age;
                model.MotherTag = cow.MotherTag;
                model.TagId = cow.TagId;
                model.BreedId = cow.BreedId;
                model.BreedName = cow.BreedId == 0 ? "" : context.Breeds.FirstOrDefault(a => a.Id == cow.BreedId).BreedName;
                model.CalfGender = cow.CalfGender;
                model.Origin = cow.Origin;
                model.Weight = cow.Weight;
                model.CowTeeth = cow.CowTeeth;
                model.Color = cow.Color;
                model.ShedNo = cow.ShedNo;
                model.LineNo = cow.LineNo;

                model.LivestockTypeVal = cow.LivestockTypeVal;


                if (model.LivestockTypeVal != null)
                {
                    model.LivestockTypeName = Enum.GetName(typeof(LivestockType), model.LivestockTypeVal);
                }
                if (model.CalfGender != null)
                {
                    model.CalfGenderName = Enum.GetName(typeof(Gender), model.CalfGender);
                }

                lists.Add(model);
            }


            return lists;
        }

        public async Task<CowServiceViewModel> GetById(long id)
        {
            var cow = await context.Cows.FindAsync(id);
            CowServiceViewModel model = new CowServiceViewModel();
            if (cow != null)
            {
                model.PurchaseDate = cow.PurchaseDate;
                model.Price = cow.Price;
                model.Id = cow.Id;
                model.Age = cow.Age;
                model.MotherTag = cow.MotherTag;
                model.TagId = cow.TagId;
                model.Weight = cow.Weight;
                model.CalfGender = cow.CalfGender;
                model.Origin = cow.Origin;
                model.CowTeeth = cow.CowTeeth;
                model.Color = cow.Color;
                model.ShedNo = cow.ShedNo;
                model.LineNo = cow.LineNo;
                model.LivestockTypeVal = cow.LivestockTypeVal;
                model.BreedId = cow.BreedId;
                if (model.LivestockTypeVal != null)
                {
                    model.LivestockTypeName = Enum.GetName(typeof(LivestockType), model.LivestockTypeVal);
                }
                if (model.CalfGender != null)
                {
                    model.CalfGenderName = Enum.GetName(typeof(Gender), model.CalfGender);
                }

                if (model.BreedId != null)
                {
                    model.BreedName = "";
                }
            }


            return model;

        }

        public async Task<CowServiceViewModel> UpdateCow(CowServiceViewModel model)
        {

            var isTagIdExists = await context.Cows.AsQueryable().AsNoTracking().Where(c => c.TagId == model.TagId && (c.IsActive == true)).ToListAsync();
            if (isTagIdExists.Count() > 1)
            {
                model.ErrorMessage = "TagId already exists. Please choose a unique TagId.";
                return model;
            }

            try
            {
                Cow cow = context.Cows.AsNoTracking().FirstOrDefault(c => c.Id == model.Id);
                if (cow != null)
                {
                    cow.PurchaseDate = model.PurchaseDate;
                    cow.CalfGender = model.CalfGender;
                    cow.Age = model.Age;
                    cow.BreedId = model.BreedId;
                    cow.IsActive = true;
                    cow.Price = model.Price;
                    cow.MotherTag = model.MotherTag;
                    cow.Origin = model.Origin;
                    cow.TagId = model.TagId;
                    cow.Weight = model.Weight;
                    cow.CowTeeth = model.CowTeeth;
                    cow.Color = model.Color;
                    cow.ShedNo = model.ShedNo;
                    cow.LineNo = model.LineNo;
                    cow.LivestockTypeVal = model.LivestockTypeVal;
                    cow.UpdatedOn = DateTime.Now;
                    context.Entry(cow).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }



                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new cow. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<bool> Remove(long id)
        {
            Cow cow = await context.Cows.FirstOrDefaultAsync(c => c.Id == id);
            cow.IsActive = false;
            context.Entry(cow).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        //Cow Report
        public async Task<CowServiceViewModel> GetCowHistoryById(long id)
        {
            var cow = await context.Cows.FindAsync(id);
            CowServiceViewModel model = new CowServiceViewModel();
            if (cow != null)
            {
                model.PurchaseDate = cow.PurchaseDate;
                model.Price = cow.Price;
                model.Id = cow.Id;
                model.Age = cow.Age;
                model.MotherTag = cow.MotherTag;
                model.TagId = cow.TagId;
                model.Weight = cow.Weight;
                model.CalfGender = cow.CalfGender;
                model.Origin = cow.Origin;
                model.CowTeeth = cow.CowTeeth;
                model.Color = cow.Color;
                model.ShedNo = cow.ShedNo;
                model.LineNo = cow.LineNo;
                model.LivestockTypeVal = cow.LivestockTypeVal;
                model.BreedId = cow.BreedId;
                model.BreedName = cow.BreedId == 0 ? "" : context.Breeds.FirstOrDefault(a => a.Id == cow.BreedId).BreedName;
                if (model.LivestockTypeVal != null)
                {
                    model.LivestockTypeName = Enum.GetName(typeof(LivestockType), model.LivestockTypeVal);
                }
                if (model.CalfGender != null)
                {
                    model.CalfGenderName = Enum.GetName(typeof(Gender), model.CalfGender);
                }




                //History by cow Id Started
                //Initializing objects
                model.cowSummaryVM = new CowSummaryVM();
                List<CowSummaryVM> feedList = new List<CowSummaryVM>();
                model.vaccineVMList = new List<VaccineServiceViewModel>();
                model.treatmentVM = new TreatmentServiceViewModel();
                model.treatmentVMList = new List<TreatmentServiceViewModel>();
                model.feedConsumptionCowWiseVM = new FeedConsumptionCowWiseServiceVM();
                model.milkServiceVM = new MilkServiceViewModel();

                //Getting required data
                var vaccines = await context.Vaccines.Where(v => v.IsActive == true && v.IsComplete == true && v.CowId == model.Id).ToListAsync();
                var treatments = await context.Treatments.Where(v => v.IsActive == true && v.IsComplete == true && v.CowId == model.Id).ToListAsync();
                var feedConsumed = await context.FeedConsumptionCowWises.Where(v => v.IsActive == true && v.CowId == model.Id).ToListAsync();
                var milkproduced = await context.MilkMonitors.Where(v => v.IsActive == true && v.CowId == model.Id).ToListAsync();

                // Bilnding data to view started here
                //Vaccine data
                model.cowSummaryVM.TotalExpense = (decimal?)cow.Price;
                if (vaccines != null)
                {
                    foreach (var vaccine in vaccines)
                    {
                        model.vaccineVM = new VaccineServiceViewModel();
                        model.vaccineVM.VaccineDate = vaccine.VaccineDate;
                        model.vaccineVM.Name = vaccine.Name;
                        model.vaccineVM.DoctorId = vaccine.DoctorId;
                        model.vaccineVM.DoctorName = vaccine.DoctorId == 0 ? "" : context.Doctors.FirstOrDefault(d => d.Id == vaccine.DoctorId).DoctorName;
                        model.vaccineVM.Price = vaccine.Price;
                        model.vaccineVMList.Add(model.vaccineVM);
                    }
                    model.cowSummaryVM.TotalVaccineCost = vaccines.Select(c => c.Price).Sum();
                    model.cowSummaryVM.TotalExpense = model.cowSummaryVM.TotalExpense + model.cowSummaryVM.TotalVaccineCost;
                }
                // Treatement data
                if (treatments != null)
                {
                    foreach (var treatment in treatments)
                    {
                        model.treatmentVM = new TreatmentServiceViewModel();
                        model.treatmentVM.TreatmentDate = treatment.TreatmentDate;
                        model.treatmentVM.TreatmentDate = treatment.TreatmentDate;
                        model.treatmentVM.DoctorId = treatment.DoctorId;
                        model.treatmentVM.DoctorName = treatment.DoctorId == 0 ? "" : context.Doctors.FirstOrDefault(d => d.Id == treatment.DoctorId).DoctorName;
                        model.treatmentVM.Price = treatment.Price;
                        model.treatmentVM.TreatmentDetails = treatment.TreatmentDetails;
                        model.treatmentVMList.Add(model.treatmentVM);
                    }
                    model.cowSummaryVM.TotalTreatmentCost = (decimal)treatments.Select(c => c.Price).Sum();
                    model.cowSummaryVM.TotalExpense = model.cowSummaryVM.TotalExpense + model.cowSummaryVM.TotalTreatmentCost;
                }
                //Cow wise feed consumed information to view
                if (feedConsumed != null)
                {
                    foreach (var feed in feedConsumed)
                    {
                        //geting single entries and saving as list to get the sum
                        model.cowSummaryVM.TotalFeedingCost = feed.Quantity * feed.UnitPrice;
                        feedList.Add(model.cowSummaryVM);
                    }
                    model.cowSummaryVM.TotalFeedingQuantity = feedConsumed.Select(c => c.Quantity).Sum();
                    model.cowSummaryVM.TotalFeedingCost = feedList.Select(c => c.TotalFeedingCost).Sum();
                    model.cowSummaryVM.TotalExpense = model.cowSummaryVM.TotalExpense + model.cowSummaryVM.TotalFeedingCost;
                    model.cowSummaryVM.TotalExpense = model.cowSummaryVM.TotalExpense;
                }
                //Cow Milk DATA to view
                if (milkproduced != null)
                {
                    model.cowSummaryVM.TotalMilkProduced = milkproduced.Select(m => m.TotalMilk).Sum();
                }


            }


            return model;

        }

        public async Task<CowSummaryVM> CowSummary30Days()
        {
            var cowsData = await context.Cows.AsQueryable().AsNoTracking().Where(c => c.IsActive == true)
                 .GroupBy(c => c.LivestockTypeVal).Select(c =>
                 new
                 {
                     LivestockType = (LivestockType)c.Key,
                     Count = c.Count()
                 }).ToListAsync();

            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);
            var last30Days = today.AddDays(-30);
            var yesterDayTotalMilk = await context.MilkMonitors.AsQueryable().AsNoTracking().Where(c => c.IsActive == true)
                          .Where(c => c.Date == yesterday).Select(c => c.TotalMilk).ToListAsync();
            var last30DaysMilk = await context.MilkMonitors.AsNoTracking().Where(c => c.IsActive == true)
                           .Where(c => c.Date >= last30Days && c.Date <= today).Select(c => c.TotalMilk).ToListAsync();
            last30DaysMilk.Sum();
            var last30DaysTreatment = await context.Treatments.AsQueryable().AsNoTracking().Where(c => c.IsActive == true)
               .Where(c => c.TreatmentDate >= last30Days && c.TreatmentDate <= today).Select(c => c.Price).ToListAsync();

            var last30DaysVaccine = await context.Vaccines.AsQueryable().AsNoTracking().Where(c => c.IsActive == true)
               .Where(c => c.VaccineDate >= last30Days && c.VaccineDate <= today).Select(c => c.Price).ToListAsync();

            var last30DaysFeed = await context.FeedConsumptionCowWises.AsQueryable().AsNoTracking().Where(c => c.IsActive == true)
               .Where(c => c.Date >= last30Days && c.Date <= today).Select(c => new { Quantity = c.Quantity, UnitPrice = c.UnitPrice }).ToListAsync();

            var model = new CowSummaryVM();
            try
            {
                foreach (var cow in cowsData)
                {
                    model.Totalcow = cow.LivestockType == (LivestockType)2 ? cow.Count : model.Totalcow;
                    model.TotalOX = cow.LivestockType == (LivestockType)1 ? cow.Count : model.TotalOX;
                    model.TotalCalf = cow.LivestockType == (LivestockType)4 ? cow.Count : model.TotalCalf;
                    model.TotalHeifer = cow.LivestockType == (LivestockType)3 ? cow.Count : model.TotalHeifer;
                }
            }
            catch
            {

            }

            model.TotalMilkProduced = yesterDayTotalMilk.Sum();
            model.TotalMilkProduced = last30DaysMilk.Sum();
            model.TotalTreatmentCost = last30DaysTreatment.Sum() + last30DaysVaccine.Sum() ?? 0;
            model.TotalFeedingCost = last30DaysFeed.Sum(c => c.Quantity * c.UnitPrice);

            return model;
        }
    }
}
