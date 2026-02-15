using Firm.Service.Services.FeedCategory_Services;
using Firm.Service.Services.FeedConsumptionBulk_Services;
using Firm.Service.Services.FeedEntry_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FirmWebApp.Controllers.Cow
{
    public class FeedConsumptionBulkController : Controller
    {
        public readonly IFeedCategoryService feedCategoryService;
        public readonly IFeedConsumptionBulkService  feedConsumptionBulkService;
        public FeedConsumptionBulkController(IFeedCategoryService feedCategoryService, IFeedConsumptionBulkService feedConsumptionBulkService)
        {
            this.feedCategoryService = feedCategoryService;
            this.feedConsumptionBulkService = feedConsumptionBulkService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            var data = await feedConsumptionBulkService.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            return View();
        } 
        
        
        public async Task<IActionResult> ShadeLineFeedList(FeedConsumptionBulkServiceVM model)
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");

             model.feedConsumptionList = await feedConsumptionBulkService.ShadeLineFeedList();
            


            return View("Views/FeedConsumptionBulk/Create.cshtml",model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(FeedConsumptionBulkServiceVM model)
        {
            try
            {
                var result = await feedConsumptionBulkService.AddNewFeedConsumptionBulk(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
                    return View(model);
                }

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
            var obj = await feedConsumptionBulkService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FeedConsumptionBulkServiceVM model)
        {
            try
            {
                var result = await feedConsumptionBulkService.UpdateFeedConsumptionBulk(model);
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
            var obj = await feedConsumptionBulkService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
