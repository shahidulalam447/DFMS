using Firm.Core.EntityModel;
using Firm.Infrastructure.Data;
using Firm.Service.Services.JobInformation_Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Employee_Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly FirmDBContext context;
        public EmployeeService(FirmDBContext context)
        {
            this.context = context;
        }
        public async Task<EmployeeServiceVM> AddNewEmployee(EmployeeServiceVM model)
        {

            bool isExists=await context.Employees.AnyAsync(c=>c.FirstName==model.FirstName);
            if (isExists)
            {
                model.ErrorMessage = "Employee already exists. Please add a unique Employee.";
                return model;
            }
            try
            {
                Employee employee=new Employee();
                employee.EmployeeId = GetUniqueRequisitionNo();
                employee.FirstName=model.FirstName;
                employee.LastName=model.LastName;
                employee.Age=model.Age;
                employee.DateOfBirth=model.DateOfBirth;
                employee.FatherName=model.FatherName;
                employee.MotherName=model.MotherName;
                employee.SpouseName=model.SpouseName;
                employee.DateOfMarriage=model.DateOfMarriage;
                employee.NationalId=model.NationalId;
                employee.Gender=model.Gender;
                employee.MaritalStatus=model.MaritalStatus;
                employee.Religion=model.Religion;
                employee.BloodGroup=model.BloodGroup;
                employee.MobileNo=model.MobileNo;
                employee.Telephone=model.Telephone;
                employee.Email=model.Email;
                employee.OfficeEmail=model.OfficeEmail;
                employee.FaxNo=model.FaxNo;
                employee.PABX=model.PABX;
                employee.DrivingLicenseNo=model.DrivingLicenseNo;
                employee.PassportNo=model.PassportNo;
                employee.TinNo=model.TinNo;
                employee.PresentAddress=model.PresentAddress;
                employee.PermanentAddress =model.PermanentAddress;
                employee.ImageFileName = model.ImageFileName;
                employee.SignatureFileName = model.SignatureFileName;
                employee.EndReason=model.EndReason;
                employee.EmployeeOrder=model.EmployeeOrder;
                //employee.ImagePath=model.ImagePath;
                employee.SignaturePath = model.SignaturePath;
                employee.SearchText=model.SearchText;
                employee.StrJoiningDate=model.StrJoiningDate;
                employee.DepartmentName=model.DepartmentName;
                employee.Image = model.Image;
                employee.CreatedOn=DateTime.Now;
                //employee.IsActive = model.IsActive;
                employee.IsDeleted = false;
                context.Employees.Add(employee);
                var res = await context.SaveChangesAsync();
                if (res !=0)
                {
                    model.Id=employee.Id;
                }
                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new Employee. Details:" + ex.Message;
                return model;
            }

            //Generate unique EmployeeId
            string GetUniqueRequisitionNo()
            {
                string prefix = "DFMS";
                string uniqueIdentifier = Guid.NewGuid().ToString("N").Substring(0, 5);
                string generatedNumber = $"{prefix.ToUpper()}-{DateTime.Now:yyMMdd}-{uniqueIdentifier}";
                return generatedNumber;
            }
        }

        public async Task<List<EmployeeServiceVM>> GetAll()
        {
            List<EmployeeServiceVM> lists = new List<EmployeeServiceVM>();
            var data=await context.Employees.ToListAsync();

            foreach (var employee in data)
            {
                EmployeeServiceVM model = new EmployeeServiceVM();
                model.EmployeeId = employee.EmployeeId;
                model.Id = employee.Id;
                model.FirstName= employee.FirstName;
                model.LastName = employee.LastName;          
                model.Age = employee.Age;
                model.DateOfBirth = employee.DateOfBirth;
                model.FatherName = employee.FatherName;
                model.MotherName = employee.MotherName;
                model.SpouseName = employee.SpouseName;
                model.DateOfMarriage = employee.DateOfMarriage;
                model.NationalId = employee.NationalId;
                model.Gender = employee.Gender;
                model.MaritalStatus = employee.MaritalStatus;
                model.Religion = employee.Religion;
                model.BloodGroup = employee.BloodGroup;
                model.MobileNo = employee.MobileNo;
                model.Telephone = employee.Telephone;
                model.Email = employee.Email;
                model.OfficeEmail = employee.OfficeEmail; 
                model.FaxNo = employee.FaxNo;
                model.PABX = employee.PABX;
                model.DrivingLicenseNo = employee.DrivingLicenseNo;
                model.PassportNo = employee.PassportNo;
                model.TinNo = employee.TinNo;
                model.PresentAddress = employee.PresentAddress;
                model.PermanentAddress = employee.PermanentAddress;
                model.ImageFileName = employee.ImageFileName;
                model.SignatureFileName = employee.SignatureFileName;
                model.EndReason = employee.EndReason;
                model.EmployeeOrder = employee.EmployeeOrder;
                //model.ImagePath = employee.ImagePath;
                model.SignaturePath = employee.SignaturePath;
                model.SearchText = employee.SearchText;
                model.StrJoiningDate = employee.StrJoiningDate;
                model.DepartmentName = employee.DepartmentName;
                model.Image=employee.Image;
                model.IsActive = employee.IsActive;
                model.IsDeleted = employee.IsDeleted;
                lists.Add(model);
            }
            return lists;
            
        }

        public async Task<EmployeeServiceVM> GetById(long id)
        {
            var employee = await context.Employees.FindAsync(id);
            EmployeeServiceVM model = new EmployeeServiceVM();
            if (employee !=null)
            {
                model.EmployeeId = employee.EmployeeId;
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;
                model.Age = employee.Age;
                model.DateOfBirth = employee.DateOfBirth;
                model.FatherName = employee.FatherName;
                model.MotherName = employee.MotherName;
                model.SpouseName = employee.SpouseName;
                model.DateOfMarriage = employee.DateOfMarriage;
                model.NationalId = employee.NationalId;
                model.Gender = employee.Gender;
                model.MaritalStatus = employee.MaritalStatus;
                model.Religion = employee.Religion;
                model.BloodGroup = employee.BloodGroup;
                model.MobileNo = employee.MobileNo;
                model.Telephone = employee.Telephone;
                model.Email = employee.Email;
                model.OfficeEmail = employee.OfficeEmail;
                model.FaxNo = employee.FaxNo;
                model.PABX = employee.PABX;
                model.DrivingLicenseNo = employee.DrivingLicenseNo;
                model.PassportNo = employee.PassportNo;
                model.TinNo = employee.TinNo;
                model.PresentAddress = employee.PresentAddress;
                model.PermanentAddress = employee.PermanentAddress;
                model.ImageFileName = employee.ImageFileName;
                model.SignatureFileName = employee.SignatureFileName;
                model.EndReason = employee.EndReason;
                model.EmployeeOrder = employee.EmployeeOrder;
                //model.ImagePath = employee.ImagePath;
                model.SignaturePath = employee.SignaturePath;
                model.SearchText = employee.SearchText;
                model.StrJoiningDate = employee.StrJoiningDate;
                model.DepartmentName = employee.DepartmentName;
                model.Image = employee.Image;
                model.Id = id;
            }
            return model;
            
        }

        public async Task<EmployeeServiceVM> UpdateEmployee(EmployeeServiceVM model)
        {

            try
            {
                var employee = await context.Employees.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (employee !=null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Age = model.Age;
                    employee.DateOfBirth = model.DateOfBirth;
                    employee.FatherName = model.FatherName;
                    employee.MotherName = model.MotherName;
                    employee.SpouseName = model.SpouseName;
                    employee.DateOfMarriage = model.DateOfMarriage;
                    employee.NationalId = model.NationalId;
                    employee.Gender = model.Gender;
                    employee.MaritalStatus = model.MaritalStatus;
                    employee.Religion = model.Religion;
                    employee.BloodGroup = model.BloodGroup;
                    employee.MobileNo = model.MobileNo;
                    employee.Telephone = model.Telephone;
                    employee.Email = model.Email;
                    employee.OfficeEmail = model.OfficeEmail;
                    employee.FaxNo = model.FaxNo;
                    employee.PABX = model.PABX;
                    employee.DrivingLicenseNo = model.DrivingLicenseNo;
                    employee.PassportNo = model.PassportNo;
                    employee.TinNo = model.TinNo;
                    employee.PresentAddress = model.PresentAddress;
                    employee.PermanentAddress = model.PermanentAddress;
                    employee.ImageFileName = model.ImageFileName;
                    employee.SignatureFileName = model.SignatureFileName;
                    employee.EndReason = model.EndReason;
                    employee.EmployeeOrder = model.EmployeeOrder;
                    //employee.ImagePath = model.ImagePath;
                    employee.SignaturePath = model.SignaturePath;
                    employee.SearchText = model.SearchText;
                    employee.StrJoiningDate = model.StrJoiningDate;
                    employee.DepartmentName = model.DepartmentName;
                    employee.Image = model.Image;
                    employee.CreatedOn = DateTime.Now;
                    context.Entry(employee).State= EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return model;
            }
            catch (Exception ex)
            {

                model.ErrorMessage = "Error occurred while adding a new Employee. Details: " + ex.Message;
                return model;
            }

        }
        public async Task<bool> SoftDelete(long id)
        {
            try
            {
                var employee=await this.context.Employees.SingleOrDefaultAsync(x=>x.Id==id);
                if (employee !=null)
                {
                    employee.IsDeleted = true;
                    this.context.Entry(employee).State= EntityState.Modified;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch 
            {

                return false;
            }
        }
        public async Task<bool> Undo(long id)
        {
            try
            {
                var employee=await this.context.Employees.SingleOrDefaultAsync(x=>x.Id==id);
                if (employee !=null)
                {
                    employee.IsDeleted = false;
                    this.context.Entry(employee).State= EntityState.Modified;
                    await context.SaveChangesAsync();
                    return false;
                }
                return true;
            }
            catch 
            {

                return true;
            }
        }

        public async Task<bool> Remove(long id)
        {
            var employee = await context.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
