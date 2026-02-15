using Firm.Service.Services.FeedCategory_Services;
using Firm.Service.Services.FeedCurrentStock_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirmWebApp.Controllers.Cow
{
    public class FeedCurrentStockController : Controller
    {
        public readonly IFeedCategoryService feedCategoryService;
        public readonly IFeedCurrentStockService feedCurrentStockService;
        public FeedCurrentStockController(IFeedCategoryService feedCategoryService, IFeedCurrentStockService feedCurrentStockService)
        {
            this.feedCategoryService = feedCategoryService;
            this.feedCurrentStockService = feedCurrentStockService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.feedCategoryList = new SelectList((await feedCategoryService.GetAll()).Select(s => new { Id = s.Id, Name = s.FeedCategoryName }), "Id", "Name");
            var data = await feedCurrentStockService.GetAll();
            return View(data);
        }

    }
}
