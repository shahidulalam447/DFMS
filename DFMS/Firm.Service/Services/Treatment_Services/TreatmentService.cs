using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Treatment_Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly FirmDBContext context;

        public TreatmentService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<TreatmentServiceViewModel> AddNewTreatment(TreatmentServiceViewModel model)
        {

            try
            {
                Treatment treatment = new Treatment();
                treatment.IdentifyDate = model.IdentifyDate;
                treatment.NextFollowUpDate = model.NextFollowUpDate;
                treatment.TreatmentDate = model.TreatmentDate;
                treatment.CowId = model.CowId;
                treatment.DoctorId = model.DoctorId;
                treatment.Investigation = model.Investigation;
                treatment.TreatmentDetails = model.TreatmentDetails;
                treatment.Price = model.Price;
                treatment.IsComplete = model.IsComplete;
                treatment.CreatedOn = DateTime.Now;
                treatment.IsActive = true;
                context.Treatments.Add(treatment);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.Id = treatment.Id;
                }


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new treatment. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<TreatmentServiceViewModel>> GetAll()
        {
            List<TreatmentServiceViewModel> lists = new List<TreatmentServiceViewModel>();
            var data = await context.Treatments.Where(x => x.IsActive).ToListAsync();
            foreach (var treatment in data)
            {
                TreatmentServiceViewModel model = new TreatmentServiceViewModel();
                model.IdentifyDate = treatment.IdentifyDate;
                model.TreatmentDate = treatment.TreatmentDate;
                model.NextFollowUpDate = treatment.NextFollowUpDate;
                model.TreatmentDetails = treatment.TreatmentDetails;
                model.CowId = treatment.CowId;
                model.CowTagId = treatment.CowId == 0 ? "" : context.Cows.FirstOrDefault(a => a.Id == treatment.CowId).TagId;
                model.DoctorId = treatment.DoctorId;
                model.DoctorName = treatment.DoctorId == 0 ? "" : context.Doctors.FirstOrDefault(d => d.Id == treatment.DoctorId).DoctorName;
                model.Id = treatment.Id;
                model.Price = treatment.Price;
                model.Investigation = treatment.Investigation;
                model.Id = treatment.Id;
                model.IsComplete = treatment.IsComplete;
                lists.Add(model);
            }

            return lists;
        }

        public async Task<TreatmentServiceViewModel> GetById(long id)
        {
            var treatment = await context.Treatments.FindAsync(id);
            TreatmentServiceViewModel model = new TreatmentServiceViewModel();
            if (treatment != null)
            {
                model.IdentifyDate = treatment.IdentifyDate;
                model.TreatmentDate = treatment.TreatmentDate;
                model.NextFollowUpDate = treatment.NextFollowUpDate;
                model.TreatmentDetails = treatment.TreatmentDetails;
                model.CowId = treatment.CowId;
                model.CowTagId = treatment.CowId == 0 ? "" : context.Cows.FirstOrDefault(a => a.Id == treatment.CowId).TagId;
                model.DoctorId = treatment.DoctorId;
                model.DoctorName = treatment.DoctorId == 0 ? "" : context.Doctors.FirstOrDefault(a => a.Id == treatment.DoctorId).DoctorName;
                model.Id = treatment.Id;
                model.Price = treatment.Price;
                model.Investigation = treatment.Investigation;
                model.Id = treatment.Id;
            }
            return model;
        }

        public async Task<bool> Remove(long id)
        {
            var treatment = await context.Treatments.FindAsync(id);
            treatment.IsActive = false;
            context.Entry(treatment).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CompleteTreatment(long id)
        {
            var treatment = await context.Treatments.FindAsync(id);
            treatment.IsComplete = true;
            context.Entry(treatment).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<TreatmentServiceViewModel> UpdateTreatment(TreatmentServiceViewModel model)
        {

            try
            {
                var treatment = await context.Treatments.FindAsync(model.Id);
                treatment.IdentifyDate = model.IdentifyDate;
                treatment.NextFollowUpDate = model.NextFollowUpDate;
                treatment.TreatmentDate = model.TreatmentDate;
                treatment.CowId = model.CowId;
                treatment.DoctorId = model.DoctorId;
                treatment.Investigation = model.Investigation;
                treatment.TreatmentDetails = model.TreatmentDetails;
                treatment.Price = model.Price;
                treatment.IsComplete = model.IsComplete;
                treatment.UpdatedOn = DateTime.Now;
                context.Entry(treatment).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new treatment. Details: " + ex.Message;
                return model;
            }
        }
    }
}
