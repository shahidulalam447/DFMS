using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Designation_Service
{
    public interface IDesignationService
    {
        Task<DesignationServiceVM> AddNewDesignationService();
        Task<List<DesignationServiceVM>> GetAll();
        Task<DesignationServiceVM> GetById(long id);
        Task<DesignationServiceVM> UpdateDesignation(DesignationServiceVM model);
        Task<bool> Remove(long id);
    }
}
