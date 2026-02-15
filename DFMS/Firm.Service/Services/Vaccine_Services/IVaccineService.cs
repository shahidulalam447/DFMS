using Firm.Service.Services.Treatment_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Vaccine_Services
{
    public interface IVaccineService
    {
        Task<VaccineServiceViewModel> AddNewVaccine(VaccineServiceViewModel model);
        Task<List<VaccineServiceViewModel>> GetAll();
        Task<VaccineServiceViewModel> GetById(long id);
        Task<VaccineServiceViewModel> UpdateVaccine(VaccineServiceViewModel model);
        Task<bool> Remove(long id);
        Task<bool> CompleteVaccine(long id);
    }
}
