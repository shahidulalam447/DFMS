using Firm.Core.EntityModel;
using Firm.Service.Services.Doctor_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Department_Services
{
    public interface IDepartmentService
    {
        Task<DepartmentServiceVM> AddNewDepartment(DepartmentServiceVM model);
        Task<List<DepartmentServiceVM>> GetAll();
        Task<DepartmentServiceVM> GetById(long id);
        Task<DepartmentServiceVM> UpdateDepartment(DepartmentServiceVM model);
        Task<bool> SoftDelete(long id);
        Task<bool> Undo(long id);
        Task<bool> Remove(long id);
    }
}
