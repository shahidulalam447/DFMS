using Firm.Service.Services.ExpenseApproval_Services;
using Firm.Service.Services.Vaccine_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirmWebApp.Controllers.Cow
{
    public class ExpenseApprovalController : Controller
    {
        public readonly IExpenseApprovalService expenseApprovalService;
        public ExpenseApprovalController(IExpenseApprovalService expenseApprovalService)
        {
            this.expenseApprovalService = expenseApprovalService;
        }
        public async Task<IActionResult> Index()
        {
            var expappList = await expenseApprovalService.GetAll();
            ViewBag.expappList = new SelectList((expappList).Select(s => new { Id = s.Id, RefNo = s.RefNo }), "Id", "RefNo");
            var data = await expenseApprovalService.GetAll();
            return View(data);
        }

        public async Task<IActionResult> ExpenseList()
        {
            var expappList = await expenseApprovalService.GetAll();
            ViewBag.expappList = new SelectList((expappList).Select(s => new { Id = s.Id, RefNo = s.RefNo }), "Id", "RefNo");
            var data = await expenseApprovalService.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            var expappList = await expenseApprovalService.GetAll();
            ViewBag.expappList = new SelectList((expappList).Select(s => new { Id = s.Id, RefNo = s.RefNo }), "Id", "RefNo");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExpenseApprovalServiceVM model)
        {
            try
            {
                var result = await expenseApprovalService.AddNewExpenseApproval(model);
                return RedirectToAction("Index");
            }
            catch
            (Exception ex)
            {
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var expappList = await expenseApprovalService.GetAll();
            ViewBag.expappList = new SelectList((expappList).Select(s => new { Id = s.Id, RefNo = s.RefNo }), "Id", "RefNo");
            var obj = await expenseApprovalService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(ExpenseApprovalServiceVM model)
        {
            try
            {
                var result = await expenseApprovalService.UpdateExpense(model);
                return RedirectToAction("Index");
            }
            catch
            (Exception ex)
            {
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(long id)
        {
            var obj = await expenseApprovalService.Remove(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Complete(long id)
        {
            var obj = await expenseApprovalService.CompleteExpense(id);
            return RedirectToAction("Index");
        }

    }
}
