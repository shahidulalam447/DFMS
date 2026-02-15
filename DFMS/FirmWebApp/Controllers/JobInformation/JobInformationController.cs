
using Firm.Service.Services.JobInformation_Services;
using Firm.Service.Services.Vaccine_Services;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.JobInformation
{
    public class JobInformationController : Controller
    {
        public readonly IJobInformationService _jobInformationService;
        public JobInformationController(IJobInformationService jobInformationService)
        {
            _jobInformationService = jobInformationService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _jobInformationService.GetAll();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(JobInformationServiceVM model)
        {
            try
            {
                var result = await _jobInformationService.AddNewJobInformation(model);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while adding Job Information.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await _jobInformationService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(JobInformationServiceVM model)
        {
            try
            {
                var result = await _jobInformationService.UpdateJobInformation(model);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while updating JobInformation.");
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(long id)
        {
            var obj = await _jobInformationService.SoftDelete(id);

            if (obj)
            {
                TempData["Message"] = "Deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Error inside me.";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Undo(long id)
        {
            var obj=await _jobInformationService.Undo(id);
            if (obj)
            {
                TempData["Message"] = "Undo successfully.";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Error inside me.";
            return RedirectToAction("Index");
        }
       
        public async Task<IActionResult> PermanentDelete(long id)
        {
            var result = await _jobInformationService.Remove(id);
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
