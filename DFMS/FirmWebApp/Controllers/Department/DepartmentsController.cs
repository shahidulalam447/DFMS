using Firm.Service.Services.Doctor_Services;
using Firm.Service.Services.Department_Services;
using Microsoft.AspNetCore.Mvc;
using Firm.Service.Services.Employee_Services;

namespace FirmWebApp.Controllers.Department
{
    public class DepartmentsController : Controller
    {
        public readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list=await _departmentService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentServiceVM model)
        {
            try
            {
                var result = await _departmentService.AddNewDepartment(model);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while adding Department");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj=await _departmentService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentServiceVM model)
        {
            try
            {
                var result = await _departmentService.UpdateDepartment(model);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while updating Department.");
                return View(model);
            }
        }

        public async Task<IActionResult> Undo(long id)
        {
            var obj = await _departmentService.Undo(id);
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
            var obj = await _departmentService.SoftDelete(id);

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
            var result = await _departmentService.Remove(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Register your services here
            services.AddTransient<IEmployeeService, EmployeeService>();
            // Other service registrations
        }

    }
}
