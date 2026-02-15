using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.JobInformation_Services
{
    public interface IJobInformationService
    {
        Task<JobInformationServiceVM> AddNewJobInformation(JobInformationServiceVM model);
        Task<List<JobInformationServiceVM>> GetAll();
        Task<JobInformationServiceVM> GetById(long id);
        Task<JobInformationServiceVM> UpdateJobInformation(JobInformationServiceVM model);
        Task<bool> SoftDelete(long id);
        Task<bool> Undo(long id);
        Task<bool> Remove(long id);
    }
}
