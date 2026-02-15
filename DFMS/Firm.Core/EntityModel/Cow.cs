using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Core.EntityModel
{
    public class Cow : BaseEntity
    {
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public long BreedId { get; set; } = 0;
        public string TagId { get; set; }
        public int? Age { get; set; }
        public double? Weight { get; set; }
        public string Origin { get; set; }
        public string Color { get; set; }
        public int? CowTeeth { get; set; }
        public double? Price { get; set; }
        public string MotherTag { get; set; }
        public Gender? CalfGender { get; set; }
        public string ShedNo { get; set; }
        public string LineNo { get; set; }
        public LivestockType? LivestockTypeVal { get; set; }
    }
}
