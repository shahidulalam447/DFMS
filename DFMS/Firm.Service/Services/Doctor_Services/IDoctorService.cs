using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Doctor_Services
{
    public interface IDoctorService
    {
        Task<DoctorServiceViewModel> AddNewDoctor(DoctorServiceViewModel model);
        Task<List<DoctorServiceViewModel>> GetAll();
        Task<DoctorServiceViewModel> GetById(long id);
        Task<DoctorServiceViewModel> UpdateDoctor(DoctorServiceViewModel model);
        Task<bool> Remove(long id);
    }
}
