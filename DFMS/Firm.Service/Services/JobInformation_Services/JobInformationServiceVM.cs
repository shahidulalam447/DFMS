using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.JobInformation_Services
{
    public class JobInformationServiceVM
    {
        public long Id { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime ProbationEndDate { get; set; }
        public DateTime PermanentDate { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string EmployeeCategory { get; set; }
        public string JobStatus { get; set; }
        public string ServiceType { get; set; }
        public string OfficeType { get; set; }
        public string Shift { get; set; }
        public string Grade { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }
    }
}
