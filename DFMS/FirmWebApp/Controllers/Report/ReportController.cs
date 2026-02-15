using Firm.Service.Services.Milk_Services;
using Firm.Service.Services.Report_Services;
using Firm.Service.Services.Report_Services.ReportViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirmWebApp.Controllers.Report
{
    public class ReportController : Controller
    {

        public readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {


            _reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MilkReport()
        {
            MilkReportVM milkReport = new MilkReportVM();

            milkReport.StartDate = DateTime.Now.AddDays(-7);
            milkReport.EndDate = DateTime.Now;

            milkReport = await _reportService.MilkReport(milkReport);
            milkReport.StartDate = DateTime.Now.AddDays(-7);
            milkReport.EndDate = DateTime.Now;
            return View("Views/MilkReport/Index.cshtml", milkReport);
        }

        [HttpPost]
        public async Task<IActionResult> MilkReport(MilkReportVM milkReportVM)
        {

            var model = new MilkReportVM();
            model = await _reportService.MilkReport(milkReportVM);
            return View("Views/MilkReport/Index.cshtml", model);
        }


        public async Task<IActionResult> MilkReportPrint(MilkReportVM milkReportVM)
        {

            var model = new MilkReportVM();
            model = await _reportService.MilkReport(milkReportVM);
            return View("Views/MilkReport/Print.cshtml", model);
        }
        public async Task<IActionResult> FeedReport()
        {
            var feedModel = new FeddingCostReportVM();
            feedModel.StartDate = DateTime.Now.AddDays(-7);
            feedModel.EndDate = DateTime.Now;
            feedModel = await _reportService.FeddingCostReport(feedModel);
            feedModel.StartDate = DateTime.Now.AddDays(-7);
            feedModel.EndDate = DateTime.Now;

            return View("Views/FeedReport/Index.cshtml",feedModel);
        }
        [HttpPost]
        public async Task<IActionResult> FeedReport(FeddingCostReportVM feddingCostReport)
        {
            var model = await _reportService.FeddingCostReport(feddingCostReport);
            return View("Views/FeedReport/Index.cshtml", model);
        }

        public async Task<IActionResult> FeedReportSummary(FeddingCostReportVM feddingCostReport)
        {
            var model = await _reportService.FeddingCostReport(feddingCostReport);
            return View("Views/FeedReport/Print.cshtml", model);
        }

        public async Task<IActionResult> CowTotalReport()
        {
            var model = new CowCostTotalVM();
            model = await _reportService.CowCost();
            return View("Views/CowTotalReport/Index.cshtml", model);
        }

        public async Task<IActionResult> CowTotalSummary()
        {
            var model = new CowCostTotalVM();
            model = await _reportService.CowCost();
            return View("Views/CowTotalReport/Print.cshtml", model);
        }

        public async Task<IActionResult> IndividualCowReport()
        {



            return View("Views/IndividualCowReport/Index.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> IndividualCowReport(IndividualCowReportVM individualCow)
        {
            var model = new IndividualCowReportVM();
            model = await _reportService.IndividualCowSummary(individualCow);


            return View("Views/IndividualCowReport/Index.cshtml", model);
        }
        public async Task<IActionResult> MedicalExpenseReport()
        {
            var model = new Vaccine_Treatment_ReportVM();
            model.StartDate = DateTime.Now.AddDays(-15);
            model.EndDate = DateTime.Now;
            model = await _reportService.Treatment_Report(model);
            model.StartDate = DateTime.Now.AddDays(-15);
            model.EndDate = DateTime.Now;
            return View("Views/MedicalExpenseReport/Index.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> MedicalExpenseReport(Vaccine_Treatment_ReportVM vaccine_Treatment)
        {
            var model = await _reportService.Treatment_Report(vaccine_Treatment);

            return View("Views/MedicalExpenseReport/Index.cshtml", model);
        }

        public async Task<IActionResult> MedicalExpenseSummary(Vaccine_Treatment_ReportVM vaccine_Treatment)
        {
            var model = await _reportService.Treatment_Report(vaccine_Treatment);

            return View("Views/MedicalExpenseReport/Print.cshtml", model);
        }

    }
}
