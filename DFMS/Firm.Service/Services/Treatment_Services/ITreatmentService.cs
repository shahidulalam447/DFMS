using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Treatment_Services
{
    public interface ITreatmentService
    {
        Task<TreatmentServiceViewModel> AddNewTreatment(TreatmentServiceViewModel model);
        Task<List<TreatmentServiceViewModel>> GetAll();
        Task<TreatmentServiceViewModel> GetById(long id);
        Task<TreatmentServiceViewModel> UpdateTreatment(TreatmentServiceViewModel model);
        Task<bool> Remove(long id);
        Task<bool> CompleteTreatment(long id);
    }
}
