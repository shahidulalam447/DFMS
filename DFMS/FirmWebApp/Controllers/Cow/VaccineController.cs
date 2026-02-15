using Firm.Service.Services.Cow_Services;
using Firm.Service.Services.Doctor_Services;
using Firm.Service.Services.Vaccine_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirmWebApp.Controllers.Cow
{
    public class VaccineController : Controller
    {
        public readonly IDoctorService doctorService;
        public readonly IVaccineService vaccineService;
        public readonly ICowService cowService;
        public VaccineController(IVaccineService vaccineService, ICowService cowService, IDoctorService doctorService)
        {
            this.vaccineService = vaccineService;
            this.cowService = cowService;
            this.doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var docList = await doctorService.GetAll();
            var cowList = await cowService.GetAll();

            ViewBag.cowlist = new SelectList((cowList).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            ViewBag.doctorList = new SelectList((docList).Select(s => new { Id = s.Id, Name = s.DoctorName }), "Id", "Name");
            var data = await vaccineService.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            var docList = await doctorService.GetAll();
            var cowList = await cowService.GetAll();

            ViewBag.cowlist = new SelectList((cowList).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            ViewBag.doctorList = new SelectList((docList).Select(s => new { Id = s.Id, Name = s.DoctorName }), "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VaccineServiceViewModel model)
        {
            try
            {
                var result = await vaccineService.AddNewVaccine(model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding cow.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var docList = await doctorService.GetAll();
            var cowList = await cowService.GetAll();

            ViewBag.cowlist = new SelectList((cowList).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            ViewBag.doctorList = new SelectList((docList).Select(s => new { Id = s.Id, Name = s.DoctorName }), "Id", "Name");

            var obj = await vaccineService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(VaccineServiceViewModel model)
        {
            try
            {
                var result = await vaccineService.UpdateVaccine(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding cow.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await vaccineService.Remove(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Complete(long id)
        {
            var obj = await vaccineService.CompleteVaccine(id);
            return RedirectToAction("Index");
        }
    }
}
