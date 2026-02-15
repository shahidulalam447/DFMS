using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.Milk_Services
{
    public class MilkServiceViewModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }=DateTime.Now;
        public long CowId { get; set; } = 0;
        public string CowTagId { get; set; }
        public Shift ShiftVal { get; set; } 
        public string ShadeNo { get; set; }
        public string LineNo { get; set; }
        public string ShiftName { get; set; }
        public decimal? DayShift { get; set; }
        public string MilkDay { get; set; }

        public decimal? EveningShift { get; set; }
        public decimal? MourningShift { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalMilk { get; set; }
        public List<MilkServiceViewModel>? milkServiceVmList { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsActive { get;  set; }
    }
}
