using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Vaccine_Services
{
    public class VaccineService : IVaccineService
    {
        private readonly FirmDBContext context;

        public VaccineService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<VaccineServiceViewModel> AddNewVaccine(VaccineServiceViewModel model)
        {

            try
            {
                Vaccine vaccine = new Vaccine();
                vaccine.Name = model.Name;
                vaccine.VaccineDate = model.VaccineDate;
                vaccine.Price = model.Price;
                vaccine.DoseNo = model.DoseNo;
                vaccine.CowId = model.CowId;
                vaccine.DoctorId = model.DoctorId;
                vaccine.Details = model.Details;
                vaccine.IsComplete = model.IsComplete;
                vaccine.CreatedOn = DateTime.Now;
                vaccine.IsActive = true;
                context.Vaccines.Add(vaccine);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.Id = vaccine.Id;
                }


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new vaccine. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<VaccineServiceViewModel>> GetAll()
        {
            List<VaccineServiceViewModel> lists = new List<VaccineServiceViewModel>();
            var data = await context.Vaccines.Where(x => x.IsActive).ToListAsync();
            foreach (var vaccine in data)
            {
                VaccineServiceViewModel model = new VaccineServiceViewModel();
                model.Name = vaccine.Name;
                model.VaccineDate = vaccine.VaccineDate;
                model.Details = vaccine.Details;
                model.DoseNo = vaccine.DoseNo;
                model.CowId = vaccine.CowId;
                model.CowTagId = vaccine.CowId == 0 ? "" : context.Cows.FirstOrDefault(a => a.Id == vaccine.CowId).TagId;
                model.DoctorId = vaccine.DoctorId;
                model.DoctorName = vaccine.DoctorId == 0 ? "" : context.Doctors.FirstOrDefault(d => d.Id == vaccine.DoctorId).DoctorName;
                model.Id = vaccine.Id;
                model.Price = vaccine.Price;
                model.Id = vaccine.Id;
                model.IsComplete = vaccine.IsComplete;
                lists.Add(model);
            }

            return lists;
        }

        public async Task<VaccineServiceViewModel> GetById(long id)
        {
            var vaccine = await context.Vaccines.FindAsync(id);
            VaccineServiceViewModel model = new VaccineServiceViewModel();
            if (vaccine != null)
            {
                model.Name = vaccine.Name;
                model.VaccineDate = vaccine.VaccineDate;
                model.Details = vaccine.Details;
                model.DoseNo = vaccine.DoseNo;
                model.CowId = vaccine.CowId;
                model.CowTagId = vaccine.CowId == 0 ? "" : context.Cows.FirstOrDefault(a => a.Id == vaccine.CowId).TagId;
                model.DoctorId = vaccine.DoctorId;
                model.DoctorName = vaccine.DoctorId == 0 ? "" : context.Doctors.FirstOrDefault(d => d.Id == vaccine.DoctorId).DoctorName;
                model.Id = vaccine.Id;
                model.Price = vaccine.Price;
                model.Id = vaccine.Id;
                model.IsComplete = vaccine.IsComplete;
            }
            return model;
        }
        public async Task<VaccineServiceViewModel> UpdateVaccine(VaccineServiceViewModel model)
        {
            try
            {
                var vaccine = await context.Vaccines.FindAsync(model.Id);
                vaccine.Name = model.Name;
                vaccine.VaccineDate = model.VaccineDate;
                vaccine.Price = model.Price;
                vaccine.DoseNo = model.DoseNo;
                vaccine.CowId = model.CowId;
                vaccine.DoctorId = model.DoctorId;
                vaccine.Details = model.Details;
                vaccine.IsComplete = model.IsComplete;
                vaccine.CreatedOn = DateTime.Now;
                vaccine.IsActive = true;
                vaccine.UpdatedOn = DateTime.Now;
                context.Entry(vaccine).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new vaccine. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<bool> Remove(long id)
        {
            var vaccine = await context.Vaccines.FindAsync(id);
            vaccine.IsActive = false;
            context.Entry(vaccine).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CompleteVaccine(long id)
        {
            var vaccine = await context.Vaccines.FindAsync(id);
            vaccine.IsComplete = true;
            context.Entry(vaccine).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

       
    }
}
