using Firm.Service.Services.FeedCurrentStock_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.FeedCategory_Services
{
    public class FeedCategoryServiceVM : BaseVM
    {
        public string FeedCategoryName { get; set; }
        public string UnitName { get; set; }
        public FeedCurrentStockServiceVM feedCurrentStockServiceVM { get; set; }
    }
}
