using Firm.Service.Services.Employee_Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FirmWebApp.Controllers.Employee
{
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _he;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment he)
        {
            _employeeService = employeeService;
            _he = he;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _employeeService.GetAll();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeServiceVM viewData = new EmployeeServiceVM()
            {
                Id = employee.Id,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Age = employee.Age,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                NationalId = employee.NationalId,
                Gender = employee.Gender,
                MaritalStatus = employee.MaritalStatus,
                Religion = employee.Religion,
                BloodGroup = employee.BloodGroup,
                MobileNo = employee.MobileNo,
                Email = employee.Email,
                PresentAddress = employee.PresentAddress,
                PermanentAddress = employee.PermanentAddress,
                Image = employee.Image,
            };

            return View(viewData);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeServiceVM model, EmployeeServiceVM employeeServiceVM)
        {
            
            try
            {
                var file = employeeServiceVM.ImagePath;
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = Path.GetFileName(employeeServiceVM.ImagePath.FileName);
                string fileToSave = Path.Combine(webroot, folder, imgFileName);
                if (file != null)
                {
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        employeeServiceVM.ImagePath.CopyTo(stream);
                        employeeServiceVM.Image = "/" + folder + "/" + imgFileName;
                    }
                }
                var result = await _employeeService.AddNewEmployee(employeeServiceVM);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(employeeServiceVM);
                }
                _employeeService.AddNewEmployee(employeeServiceVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while adding Employee.");
                return View(employeeServiceVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj=await _employeeService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeServiceVM model, EmployeeServiceVM employeeServiceVM)
        {
            
            try
            {
                var file = employeeServiceVM.ImagePath;
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = Path.GetFileName(employeeServiceVM.ImagePath.FileName);
                string fileToSave = Path.Combine(webroot, folder, imgFileName);
                if (file != null)
                {
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        employeeServiceVM.ImagePath.CopyTo(stream);
                        employeeServiceVM.Image = "/" + folder + "/" + imgFileName;
                    }
                }

                var result = await _employeeService.UpdateEmployee(employeeServiceVM);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(employeeServiceVM);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "An error occurred while updating employee.");
                return View(employeeServiceVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Undo(long id)
        {
            var obj = await _employeeService.Undo(id);

            if (obj)
            {
                TempData["Message"] = "Undo successfully.";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Error inside me.";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(long id)
        {
            var obj = await _employeeService.SoftDelete(id);

            if (obj)
            {
                TempData["Message"] = "Deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Error inside me.";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PermanentDelete(long id)
        {
            var result = await _employeeService.Remove(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
