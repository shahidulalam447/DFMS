using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedConsumptionCowWise_Services
{
    public class FeedConsumptionCowWiseServiceVM : BaseVM
    {
        public DateTime Date { get; set; }
        public long CowId { get; set; }
        public long FeedCategoryId { get; set; }
        public string FeedCategoryName { get; set; }
        public string CowTagNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal UnitPrice { get; set; }
        public Dictionary<long, decimal> FeedCategoryQuantities { get; set; }

    }
}
