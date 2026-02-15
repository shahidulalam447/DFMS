using Firm.Service.Services.FeedConsumptionCowWise_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedConsumptionBulk_Services
{
    public class FeedConsumptionBulkServiceVM : BaseVM
    {
        public DateTime Date { get; set; }
        public long FeedCategoryId { get; set; }
        public string FeedCategoryName { get; set; }
        public int ShadeNo { get; set; }
        public int LineNo { get; set; }
        public decimal Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]

        public decimal UnitPrice { get; set; }
       public  List<FeedConsumptionBulkServiceVM> feedConsumptionList { get; set; }
    }
}
