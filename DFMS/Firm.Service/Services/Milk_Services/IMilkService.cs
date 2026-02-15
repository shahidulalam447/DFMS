using Firm.Service.Services.Vaccine_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Milk_Services
{
    public interface IMilkService
    {
        Task<MilkServiceViewModel> AddNewMilk(MilkServiceViewModel model);
        Task<List<MilkServiceViewModel>> GetAll();
        Task<MilkServiceViewModel> GetById(long id);
        Task<MilkServiceViewModel> UpdateMilk(MilkServiceViewModel model);
        Task<bool> Remove(long id);
        Task<List<MilkServiceViewModel>> MilkingCowList(MilkServiceViewModel model);
    }
}
