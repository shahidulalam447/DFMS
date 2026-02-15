using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class Treatment : BaseEntity
    {
        public DateTime IdentifyDate { get; set; }
        public DateTime TreatmentDate { get; set; } = DateTime.Now;
        public DateTime? NextFollowUpDate { get; set; }
        public long CowId { get; set; } = 0;
        public long DoctorId { get; set; } = 0; 
        public string Investigation { get; set; }
        public string TreatmentDetails { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
