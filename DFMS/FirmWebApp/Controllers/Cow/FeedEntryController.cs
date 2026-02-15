using Firm.Service.Services.Cow_Services;
using Firm.Service.Services.Doctor_Services;
using Firm.Service.Services.FeedCategory_Services;
using Firm.Service.Services.FeedEntry_Services;
using Firm.Service.Services.Vaccine_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FirmWebApp.Controllers.Cow
{
    public class FeedEntryController : Controller
    {
        public readonly IFeedCategoryService feedCategoryService;
        public readonly IFeedEntryService feedEntryService;
        public FeedEntryController(IFeedCategoryService feedCategoryService , IFeedEntryService feedEntryService)
        {
            this.feedCategoryService = feedCategoryService;
            this.feedEntryService = feedEntryService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            var data = await feedEntryService.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FeedEntryServiceVM model)
        {
            try
            {
                var result = await feedEntryService.AddNewFeedEntry(model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding Feed Entry.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            var obj = await feedEntryService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FeedEntryServiceVM model)
        {
            try
            {
                var result = await feedEntryService.UpdateFeedEntry(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding Feed Entry.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await feedEntryService.Remove(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetUnitName(int categoryId)
        {
            var feedCategory = await feedCategoryService.GetById(categoryId);
            if (feedCategory != null)
            {
                return Json(new { unitName = feedCategory.UnitName });
            }

            return Json(new { unitName = "" }); // Return an empty unitName if no category is found
        }
    }
}
