using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class Department:BaseEntity
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        //public ICollection<Designation> Designations { get; set; }=new List<Designation>();
        public bool IsDeleted { get; set; }
        //public string ErrorMessage { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
