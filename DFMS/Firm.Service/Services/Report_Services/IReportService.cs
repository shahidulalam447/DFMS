using Firm.Service.Services.Report_Services.ReportViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Report_Services
{
    public  interface IReportService
    {
        Task<Vaccine_Treatment_ReportVM> Treatment_Report(Vaccine_Treatment_ReportVM VTReportVM);
        Task<FeddingCostReportVM> FeddingCostReport(FeddingCostReportVM FeddingReport);
        Task<MilkReportVM> MilkReport(MilkReportVM milkReport);
        Task<IndividualCowReportVM> IndividualCowSummary(IndividualCowReportVM individualCow);
        Task<CowCostTotalVM> CowCost();



    }
}
