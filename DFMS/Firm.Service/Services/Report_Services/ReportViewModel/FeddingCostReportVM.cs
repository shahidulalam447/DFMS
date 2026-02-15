using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Report_Services.ReportViewModel
{
    public class FeddingCostReportVM
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Day { get; set; }
        public int TottalDay { get; set; }
        public string TagNo { get; set; }
        public decimal FoodUnit { get; set; }
        public decimal TottalFoodUnit { get; set; }
        public decimal Consumption { get; set; }
        public int TottalCow { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal TottalConsumption { get; set; }
        public List<FeddingCostReportVM> FeddingCostList { get; set; }
    }
}
