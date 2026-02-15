using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class JobInformation:BaseEntity
    {
        public int JobId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime ProbationEndDate { get; set; }
        public DateTime PermanentDate { get; set; }
        public string Department {  get; set; }
        public string Designation {  get; set; }
        public string Manager { get; set; }
        public string Company { get; set; }
        public string StoreInfo { get; set; }
        public string Country { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Upzilla { get; set; }
        public string EmployeeCategory { get; set; }
        public string MaritalType { get; set; }
        public string Relligion { get; set; }
        public string JobStatus { get; set; }
        public string ServiceType { get; set; }
        public string OfficeType { get; set; }
        public string Shift {  get; set; }
        public string Grade { get; set; }
        public bool IsDeleted { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
