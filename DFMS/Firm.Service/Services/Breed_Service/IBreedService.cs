using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Breed_Service
{
    public interface IBreedService
    {
        Task<BreedServiceViewModel> AddBreed(BreedServiceViewModel model);
        Task<List<BreedServiceViewModel>> GetAll();
        Task<BreedServiceViewModel> GetById(long id);
        Task<BreedServiceViewModel> UpdateBreed(BreedServiceViewModel model);
        Task<bool> Remove(long id);
    }
}
