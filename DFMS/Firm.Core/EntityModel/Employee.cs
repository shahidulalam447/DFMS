using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class Employee : BaseEntity

    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public Nullable<System.DateTime> DateOfMarriage { get; set; }
        public string NationalId { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion {  get; set; }
        public string BloodGroup { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string FaxNo { get; set; }
        public string PABX { get; set; }
        public string DrivingLicenseNo { get; set; }
        public string PassportNo { get; set; }
        public string TinNo { get; set; }
        public string SocialId { get; set; }
        //Employee Basice Information
        public string IssdevId { get; set; }
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int EmployeeOrder { get; set; }
        public string Remarks { get; set; }
        //Job Information
        public DateTime? JoiningDate { get; set; }
        public DateTime ProbationEndDate { get; set; }
        public DateTime PermanentDate { get; set; }
        //CantactInformation
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MobileNo { get; set; }
        public string? Image {  get; set; }
        public string ImageFileName { get; set; }
        public string SignatureFileName { get; set; }
        public string EndReason { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> GradeId { get; set; }

        //Bank Information
        [DisplayName("Bank")]
        public Nullable<int> BankId { get; set; }
        [DisplayName("Bank Branch")]
        public Nullable<int> BankBranchId { get; set; }
        [DisplayName("Bank Account")]
        public string BankAccount { get; set; }
        //public string ImagePath { get; set; }
        public string SignaturePath { get; set; }
        //------------Extendend Properties----------
        public string SearchText { get; set; }
        public string StrJoiningDate { get; set; }
        public string DepartmentName {  get; set; }
        public bool IsDeleted { get; set; }
        public List<Department> Departments { get; set; }
        public List<JobInformation> JobInformation { get; set; }
        public List<Grade> Grades { get; set; } 
    }
}
