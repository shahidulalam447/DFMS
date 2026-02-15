using Firm.Core.EntityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Employee_Services
{
    public class EmployeeServiceVM
    {
        public string EmployeeId { get; set; }
        public long Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int Age { get; set; }
        [Display(Name = "Date Of Birth")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }
        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }
        [Display(Name = "Date Of Marriage")]
        public Nullable<System.DateTime> DateOfMarriage { get; set; }
        [Display(Name = "National ID")]
        public string NationalId { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string FaxNo { get; set; }
        public string PABX { get; set; }
        public string DrivingLicenseNo { get; set; }
        public string PassportNo { get; set; }
        public string TinNo { get; set; }
        public string SocialId { get; set; }
        public string IssdevId { get; set; }
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int EmployeeOrder { get; set; }
        public string Remarks { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime ProbationEndDate { get; set; }
        public DateTime PermanentDate { get; set; }
        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }
        public string ImageFileName { get; set; }
        public string SignatureFileName { get; set; }
        public string EndReason { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> GradeId { get; set; }

        [DisplayName("Bank Branch")]
        public Nullable<int> BankBranchId { get; set; }
        [DisplayName("Bank Account")]
        public string BankAccount { get; set; }
        public string? Image { get; set; }
        public string ImageFile { get; set; }
        public IFormFile ImagePath { get; set; }
        public string SignaturePath { get; set; }
        //------------Extendend Properties----------
        public string SearchText { get; set; }
        public string StrJoiningDate { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } = true;
        public string ErrorMessage { get; set; }

    }
}
