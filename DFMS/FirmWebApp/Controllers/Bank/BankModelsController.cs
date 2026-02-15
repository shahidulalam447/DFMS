using Firm.Service.Services.Bank_Services;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.Bank
{
    public class BankModelsController : Controller
    {
        public readonly IBankService _bankService;
        public BankModelsController(IBankService bankService)
        {
            _bankService = bankService;
        }

        public async Task<IActionResult> Index()
        {
            var list=await _bankService.GetAll();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BankServiceVM model)
        {
            try
            {
                var result = await _bankService.AddNewBank(model);
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while adding Bank.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var obj=await _bankService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BankServiceVM model)
        {
            try
            {
                var result = await _bankService.UpdateBank(model);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ViewData["ErrorMessage"] = result.ErrorMessage;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while updating bank.");
                return View(model);
            }
        }

        public async Task<IActionResult> Undo(long id)
        {
            var obj = await _bankService.Undo(id);

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
            var obj = await _bankService.SoftDelete(id);

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
            var result = await _bankService.Remove(id);
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
