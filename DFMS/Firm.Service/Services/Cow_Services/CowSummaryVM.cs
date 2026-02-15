using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Cow_Services
{
    public class CowSummaryVM
    {
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? TotalExpense { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalVaccineCost { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalTreatmentCost { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalFeedingCost { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal TotalFeedingQuantity { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]

        public decimal TotalMilkProduced { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]

        public decimal YesterdayTotalMilk { get; set; } = 0;
        public int Totalcow { get; set; } = 0;
        public int TotalOX { get; set; } = 0;
        public int TotalCalf { get; set; } = 0;
        public int TotalHeifer { get; set; } = 0;

    }
}
