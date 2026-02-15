using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedEntry_Services
{
    public class FeedEntryServiceVM : BaseVM
    {
        public DateTime Date { get; set; }
        public long FeedCategoryId { get; set; }
        public string FeedCategoryName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal UnitPrice { get; set; }
    }
}
