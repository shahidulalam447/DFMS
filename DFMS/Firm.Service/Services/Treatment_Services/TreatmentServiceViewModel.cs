using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Treatment_Services
{
    public class TreatmentServiceViewModel
    {
        public DateTime IdentifyDate { get; set; }
        public DateTime TreatmentDate { get; set; } = DateTime.Now;
        public DateTime? NextFollowUpDate { get; set; }
        public long CowId { get; set; }
        public long Id { get; set; }
        public string CowTagId { get; set; }
        public string ErrorMessage { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Investigation { get; set; }
        public string TreatmentDetails { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }
        public bool IsComplete { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
