using Firm.Service.Services.FeedCategory_Services;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.Cow
{
    public class FeedCategoryController : Controller
    {
        public readonly IFeedCategoryService categoryService;
        public FeedCategoryController(IFeedCategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await categoryService.GetAllWithStock();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FeedCategoryServiceVM model)
        {
            try
            {
                var result = await categoryService.AddNewFeedCategory(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while feed category.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await categoryService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FeedCategoryServiceVM model)
        {
            try
            {
                var result = await categoryService.UpdateFeedCategory(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating feed category.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await categoryService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
