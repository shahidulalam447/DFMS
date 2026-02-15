using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Firm.Service.Services.Cow_Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.Breed_Service
{
    public class BreedService : IBreedService
    {
        private readonly FirmDBContext context;

        public BreedService (FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<BreedServiceViewModel> AddBreed(BreedServiceViewModel model)
        {
            bool isBreedExists = await context.Breeds.AnyAsync(c => c.BreedName == model.BreedName);
            if (isBreedExists)
            {
                model.ErrorMessage = "Breed already exists. Please add a unique Breed.";
                return model;
            }

            try
            {
                Breed breed = new Breed();
                breed.BreedName = model.BreedName;
                breed.CreatedOn = DateTime.Now;
                breed.IsActive = true;
                context.Breeds.Add(breed);
                var res = await context.SaveChangesAsync();

                if (res != 0)
                {
                    model.BreedId = breed.Id;
                }


                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new breed. Details: " + ex.Message;
                return model;
            }
        }

        public async Task<List<BreedServiceViewModel>> GetAll()
        {
            List<BreedServiceViewModel> lists = new List<BreedServiceViewModel>();
            var data = await context.Breeds.Where(x => x.IsActive).ToListAsync();
              foreach (var breed in data)
                {
                    BreedServiceViewModel model = new BreedServiceViewModel();
                    model.BreedId = breed.Id;
                    model.BreedName = breed.BreedName;
                    lists.Add(model);
                }
           
            return lists;
        }

        public async Task<BreedServiceViewModel> GetById(long id)
        {
            var breed = await context.Breeds.FindAsync(id);
            BreedServiceViewModel model = new BreedServiceViewModel();
            if (breed != null)
            {
                model.BreedName = breed.BreedName;
                model.BreedId = breed.Id;
            }
            return model;
        }

        public async Task<bool> Remove(long id)
        {
            var breed = await context.Breeds.FirstOrDefaultAsync(c => c.Id == id);
            breed.IsActive = false;
            context.Entry(breed).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<BreedServiceViewModel> UpdateBreed(BreedServiceViewModel model)
        {
            bool isBreedExists = await context.Breeds.AnyAsync(c => c.BreedName == model.BreedName);
            if (isBreedExists)
            {
                model.ErrorMessage = "Breed already exists. Please add a unique Breed.";
                return model;
            }

            try
            {
                var breed = await context.Breeds.FindAsync(model.BreedId);
                breed.BreedName = model.BreedName;
                breed.UpdatedOn = DateTime.Now;
                context.Entry(breed).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error occurred while adding a new breed. Details: " + ex.Message;
                return model;
            }
        }
    }
}
