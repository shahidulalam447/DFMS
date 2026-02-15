using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Core.EntityModel
{
    public class MilkMonitor : BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public long CowId { get; set; } = 0;
        public Shift ShiftVal { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalMilk { get; set; }
        public string Remarks { get; set; }
    }
}
