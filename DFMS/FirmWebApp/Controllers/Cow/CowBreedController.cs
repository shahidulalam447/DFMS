using Firm.Service.Services.Breed_Service;
using Firm.Service.Services.Cow_Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.Cow
{
    public class CowBreedController : Controller
    {
        public readonly IBreedService breedService;
        public CowBreedController(IBreedService breedService)
        {
            this.breedService = breedService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await breedService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BreedServiceViewModel model)
        {
            try
            {
                var result = await breedService.AddBreed(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding breed.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await breedService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BreedServiceViewModel model)
        {
            try
            {
                var result = await breedService.UpdateBreed(model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating breed.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await breedService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
