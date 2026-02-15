using Firm.Service.Services.Breed_Service;
using Firm.Service.Services.Cow_Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FirmWebApp.Controllers.Cow
{
    public class CowController : Controller
    {
        public readonly ICowService cowService;
        public readonly IBreedService breedService;
        public CowController(ICowService cowService, IBreedService breedService)
        {
            this.cowService = cowService;
            this.breedService = breedService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await breedService.GetAll();
            ViewBag.breedlist = new SelectList((list).Select(s => new { Id = s.BreedId, Name = s.BreedName }), "Id", "Name");
            var data = await cowService.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            var list = await breedService.GetAll();
            ViewBag.breedlist = new SelectList((list).Select(s => new { Id = s.BreedId, Name = s.BreedName }), "Id", "Name");
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CowServiceViewModel model)
        {
            try
            {
                var result = await cowService.AddNewCow(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

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
            var list = await breedService.GetAll();
            ViewBag.breedlist = new SelectList((list).Select(s => new { Id = s.BreedId, Name = s.BreedName }), "Id", "Name");

            var obj = await cowService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CowServiceViewModel model)
        {
            try
            {
                var result = await cowService.UpdateCow(model);

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
            var obj = await cowService.Remove(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CowSummary(long id)
        {
            var obj = await cowService.GetCowHistoryById(id);
            return PartialView("_CowSummary", obj);
        }


    }
}
