using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Report_Services.ReportViewModel
{
    public class CowCostTotalVM
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TagNo { get; set; }

        public decimal? CowBuying { get; set; }
        public int Cow { get; set; }

        public decimal VacCost { get; set; }
        public decimal? Treatment { get; set; }
        public decimal FeedCost { get; set; }
        public double? BuyCost { get; set; }

        public decimal? perCowCosting { get; set; }
        public decimal? TotalCowCost { get; set; }

        public decimal TotalVacCost { get; set; }
        public decimal? TotalTreatment { get; set; }
        public decimal TotalFeedCost { get; set; }

        public decimal TotalperCowCosting { get; set; }
        public IList<CowCostTotalVM> CowCostList { get; set; }
    }
}
