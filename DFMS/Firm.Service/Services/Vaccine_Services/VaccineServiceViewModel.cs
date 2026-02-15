using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Vaccine_Services
{
    public class VaccineServiceViewModel
    {
        public long Id { get; set; }
        public DateTime VaccineDate { get; set; }
        public long CowId { get; set; } = 0;
        public string CowTagId { get; set; }
        public long DoctorId { get; set; } = 0;
        public string DoctorName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int DoseNo { get; set; }
        public string Details { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsComplete { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
