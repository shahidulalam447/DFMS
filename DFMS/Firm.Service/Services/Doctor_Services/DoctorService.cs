using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Doctor_Services
{
    public class DoctorService : IDoctorService
    {
        private readonly FirmDBContext context;

        public DoctorService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<DoctorServiceViewModel> AddNewDoctor(DoctorServiceViewModel model)
        {
            bool isExists = await context.Doctors.AnyAsync(c => c.DoctorPhone == model.DoctorPhone);
            if (isExists)
            {
                model.ErrorMessage = "Doctor already exists. Please add a unique Doctor.";
                return model;
            }
            try
            {
                Doctor doctor = new Doctor();
                doctor.DoctorPhone = model.DoctorPhone;
                doctor.DoctorName = model.DoctorName;
                doctor.DoctorAddress = model.DoctorAddress;
                doctor.CreatedOn = DateTime.Now;
                doctor.IsActive = true;
                context.Doctors.Add(doctor);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.Id = doctor.Id;
                }


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new doctor. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<DoctorServiceViewModel>> GetAll()
        {
            List<DoctorServiceViewModel> lists = new List<DoctorServiceViewModel>();
            var data = await context.Doctors.Where(x => x.IsActive).ToListAsync();
            foreach (var doctor in data)
            {
                DoctorServiceViewModel model = new DoctorServiceViewModel();
                model.DoctorName = doctor.DoctorName;
                model.DoctorAddress = doctor.DoctorAddress;
                model.DoctorPhone = doctor.DoctorPhone;
                model.Id = doctor.Id;
                lists.Add(model);
            }

            return lists;
        }

        public async Task<DoctorServiceViewModel> GetById(long id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            DoctorServiceViewModel model = new DoctorServiceViewModel();
            if (doctor != null)
            {
                model.DoctorName = doctor.DoctorName;
                model.DoctorPhone = doctor?.DoctorPhone;
                model.DoctorAddress = doctor?.DoctorAddress;
                model.Id = id;
            }
            return model;
        }

        public async Task<DoctorServiceViewModel> UpdateDoctor(DoctorServiceViewModel model)
        {
            bool isExists = await context.Doctors.AnyAsync(c => c.DoctorPhone == model.DoctorPhone && c.Id != model.Id);
            if (isExists)
            {
                model.ErrorMessage = "Doctor already exists. Please add another Phone Number.";
                return model;
            }

            try
            {
                var doctor = await context.Doctors.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (doctor != null)
                {
                    doctor.DoctorPhone = model.DoctorPhone;
                    doctor.DoctorName = model.DoctorName;
                    doctor.DoctorAddress = model.DoctorAddress;
                    doctor.UpdatedOn = DateTime.Now;
                    context.Entry(doctor).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
              
                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new doctor. Details: " + ex.Message;
                return model;
            }
        }
        public async Task<bool> Remove(long id)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(c => c.Id == id);
            doctor.IsActive = false;
            context.Entry(doctor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
