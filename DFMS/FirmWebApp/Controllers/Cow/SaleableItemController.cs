using Firm.Service.Services.SaleableItem_Services;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.Cow
{
    public class SaleableItemController : Controller
    {
        public readonly ISaleableItemService saleableItemService;
        public SaleableItemController(ISaleableItemService saleableItemService)
        {
            this.saleableItemService = saleableItemService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await saleableItemService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaleableItemServiceVM model)
        {
            try
            {
                var result = await saleableItemService.AddNewSaleableItem(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while new item.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj = await saleableItemService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaleableItemServiceVM model)
        {
            try
            {
                var result = await saleableItemService.UpdateSaleableItem(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating item.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await saleableItemService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
