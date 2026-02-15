using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Grade_Service
{
    public class GradeServiceVM
    {
        public long Id { get; set; }
        [Display(Name ="Please Enter your Grade no")]
        public string GradeNo { get; set; }
        [Display(Name = "Please Enter your Designation Name")]
        public string Designation { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal IncrementAmount { get; set; }
        public string IncrementNumber { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }
    }
}
