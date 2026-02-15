using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedCurrentStock_Services
{
    public class FeedCurrentStockServiceVM : BaseVM
    {
        public DateTime Date { get; set; }
        public long FeedCategoryId { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CurrentQuantity { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CurrentUnitPrice { get; set; }= 0;
        public string FeedCategoryName { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
