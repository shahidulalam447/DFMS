using Firm.Service.Services.Cow_Services;
using Firm.Service.Services.FeedCategory_Services;
using Firm.Service.Services.FeedConsumptionCowWise_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirmWebApp.Controllers.Cow
{
    public class FeedConsumptionCowWiseController : Controller
    {
        public readonly IFeedCategoryService feedCategoryService;
        public readonly ICowService cowService;
        public readonly IFeedConsumptionCowWiseService feedConsumptionCowWiseService;
        public FeedConsumptionCowWiseController(IFeedCategoryService feedCategoryService, IFeedConsumptionCowWiseService feedConsumptionCowWiseService, ICowService cowService)
        {
            this.feedCategoryService = feedCategoryService;
            this.feedConsumptionCowWiseService = feedConsumptionCowWiseService;
            this.cowService = cowService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            ViewBag.cowList = new SelectList((await cowService.GetAll()).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            var data = await feedConsumptionCowWiseService.GetAll();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            ViewBag.cowList = new SelectList((await cowService.GetAll()).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FeedConsumptionCowWiseServiceVM model)
        {
            try
            {
                var result = await feedConsumptionCowWiseService.AddNewFeedConsumptionCowWiseService(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
                    ViewBag.cowList = new SelectList((await cowService.GetAll()).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding data.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            ViewBag.cowList = new SelectList((await cowService.GetAll()).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");

            var obj = await feedConsumptionCowWiseService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FeedConsumptionCowWiseServiceVM model)
        {
            try
            {
                var result = await feedConsumptionCowWiseService.UpdateFeedConsumptionCowWiseService(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding data.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await feedConsumptionCowWiseService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
