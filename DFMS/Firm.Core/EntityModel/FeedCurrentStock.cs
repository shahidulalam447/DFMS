using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class FeedCurrentStock : BaseEntity
    {
        public DateTime Date { get; set; }
        public long FeedCategoryId { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CurrentQuantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CurrentUnitPrice { get; set; }
    }
}
