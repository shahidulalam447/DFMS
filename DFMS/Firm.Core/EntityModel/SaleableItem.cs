using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class SaleableItem : BaseEntity
    {
        public string ItemName { get; set; }
        public string UnitName { get; set; }
    }
}
