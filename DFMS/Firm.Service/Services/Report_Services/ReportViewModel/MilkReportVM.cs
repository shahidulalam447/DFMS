using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Report_Services.ReportViewModel
{
    public class MilkReportVM
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CowTagId { get; set; }
        public int TotalCow { get; set; }
        public string Day { get; set; }
        public int tottalDay { get; set; }
        public decimal TotalCowMilk { get; set; }
        public decimal TotalMilk { get; set; }

        public List<MilkReportVM> ListMilkReportVM { get; set; }

    }
}
