using Firm.Service.Services.FeedConsumptionCowWise_Services;
using Firm.Service.Services.Milk_Services;
using Firm.Service.Services.Treatment_Services;
using Firm.Service.Services.Vaccine_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.Cow_Services
{
    public class CowServiceViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public long BreedId { get; set; } = 0;
        public string BreedName { get; set; }
        //public string Breed { get; set; }
        public string TagId { get; set; }
        public int? Age { get; set; } = 0;
        public double? Weight { get; set; }
        public string? Origin { get; set; }
        public double? Price { get; set; }
        public string MotherTag { get; set; }
        public Gender? CalfGender { get; set; }
        public string CalfGenderName { get; set; }
        public bool? IsActive { get; set; }
        public int? CowTeeth { get; set; }
        public string Color { get; set; }
        public string ErrorMessage { get; internal set; }
        public string ShedNo { get; set; }
        public string LineNo { get; set; }
        public LivestockType? LivestockTypeVal { get; set; }
        public string LivestockTypeName { get; set; }
        public VaccineServiceViewModel vaccineVM { get; set; }
        public List<VaccineServiceViewModel> vaccineVMList { get; set; }
        public TreatmentServiceViewModel treatmentVM { get; set; }
        public List<TreatmentServiceViewModel> treatmentVMList { get; set; }
        public FeedConsumptionCowWiseServiceVM feedConsumptionCowWiseVM { get; set; }
        public MilkServiceViewModel milkServiceVM { get; set; }
        public CowSummaryVM cowSummaryVM { get; set; }




    }
}
