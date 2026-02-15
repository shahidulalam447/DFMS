using Firm.Core.EntityModel;
using Firm.Service.Services.Cow_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Employee_Services
{
    public interface IEmployeeService
    {
        
        Task<EmployeeServiceVM> AddNewEmployee(EmployeeServiceVM model);
        Task<List<EmployeeServiceVM>> GetAll();
        Task<EmployeeServiceVM> GetById(long id);
        Task<EmployeeServiceVM> UpdateEmployee(EmployeeServiceVM model);
        Task<bool> SoftDelete(long id);
        Task<bool> Undo(long id);
        Task<bool> Remove(long id);

     }
} 