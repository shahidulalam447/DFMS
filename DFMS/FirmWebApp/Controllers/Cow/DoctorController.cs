using Firm.Service.Services.Doctor_Services;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.Cow
{
    public class DoctorController : Controller
    {
        public readonly IDoctorService doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await doctorService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorServiceViewModel model)
        {
            try
            {
                var result = await doctorService.AddNewDoctor(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding doctor.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await doctorService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorServiceViewModel model)
        {
            try
            {
                var result = await doctorService.UpdateDoctor(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating doctor.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await doctorService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
