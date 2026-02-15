using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class Grade:BaseEntity
    {
        public int GradeId { get; set; }
        public string GradeNo { get; set; }
        public string Designation { get; set;}
        public decimal MinSalary {  get; set;}
        public decimal MaxSalary { get; set;}
        public decimal IncrementAmount { get; set;}
        public string IncrementNumber { get; set;}
        public bool IsDeleted { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
