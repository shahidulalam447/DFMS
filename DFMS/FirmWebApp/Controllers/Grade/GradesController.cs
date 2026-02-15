using Firm.Service.Services.Doctor_Services;
using Firm.Service.Services.Grade_Service;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.Grade
{
    public class GradesController : Controller
    {
        public readonly IGradeService _gradeService;
        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        public async Task<IActionResult> Index()
        {
            var list=await _gradeService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(GradeServiceVM model)
        {
            try
            {
                var result = await _gradeService.AddNewGrade(model);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while adding Grade.");
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await _gradeService.GetById(id);
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GradeServiceVM model)
        {
            try
            {
                var result = await _gradeService.UpdateGrade(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while updating grade.");
                return View(model);
            }
        }

        public async Task<IActionResult> Undo(long id)
        {
            var obj = await _gradeService.Undo(id);

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
            var obj=await _gradeService.SoftDelete(id);

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
            var result=await _gradeService.Remove(id);
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
