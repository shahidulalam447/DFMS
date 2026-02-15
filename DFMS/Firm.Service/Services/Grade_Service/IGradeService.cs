using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Grade_Service
{
    public interface IGradeService
    {
        Task<GradeServiceVM> AddNewGrade(GradeServiceVM model);
        Task<List<GradeServiceVM>> GetAll();
        Task<GradeServiceVM> GetById(long id);
        Task<GradeServiceVM> UpdateGrade(GradeServiceVM model);
        Task<bool> SoftDelete(long id);
        Task<bool> Undo(long id);
        Task<bool> Remove(long id);
    }
}
