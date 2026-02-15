using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.Report_Services.ReportViewModel
{
    public class IndividualCowReportVM
    {
        public int TagId { get; set; }
        public decimal Weight { get; set; }
        public decimal CurrentMeatPrice { get; set; }
        public double? BuyCost { get; set; }

        public decimal? TotalCowCost { get; set; }
        public decimal? CowPrice { get; set; }

        public decimal TotalVacCost { get; set; }
        public decimal TotalMilkEarn { get; set; }
        public LivestockType LivestockType { get; set; }
        public decimal? TotalTreatment { get; set; }
        public decimal TotalFeedCost { get; set; }
    }
}
