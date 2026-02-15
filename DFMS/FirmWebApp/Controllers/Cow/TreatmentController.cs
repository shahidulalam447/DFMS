using Firm.Service.Services.Cow_Services;
using Firm.Service.Services.Doctor_Services;
using Firm.Service.Services.Treatment_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirmWebApp.Controllers.Cow
{
    public class TreatmentController : Controller
    {
        public readonly IDoctorService doctorService;
        public readonly ITreatmentService treatmentService;
        public readonly ICowService cowService;
        public TreatmentController(ITreatmentService treatmentService, ICowService cowService, IDoctorService doctorService)
        {
            this.treatmentService = treatmentService;
            this.cowService = cowService;
            this.doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var docList = await doctorService.GetAll();
            var cowList = await cowService.GetAll();

            ViewBag.cowlist = new SelectList((cowList).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            ViewBag.doctorList = new SelectList((docList).Select(s => new { Id = s.Id, Name = s.DoctorName }), "Id", "Name");
            var data = await treatmentService.GetAll();
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
        public async Task<IActionResult> Create(TreatmentServiceViewModel model)
        {
            try
            {
                var result = await treatmentService.AddNewTreatment(model);

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

            var obj = await treatmentService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TreatmentServiceViewModel model)
        {
            try
            {
                var result = await treatmentService.UpdateTreatment(model);

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
            var obj = await treatmentService.Remove(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Complete(long id)
        {
            var obj = await treatmentService.CompleteTreatment(id);
            return RedirectToAction("Index");
        }
    }
}
