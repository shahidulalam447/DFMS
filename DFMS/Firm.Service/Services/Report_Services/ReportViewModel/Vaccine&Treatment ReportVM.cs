using Firm.Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Report_Services.ReportViewModel
{
    public class Vaccine_Treatment_ReportVM
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Day { get; set; }
        public string TagId { get; set; }
        public string TreatmentFor { get; set; }
        public string CostingType { get; set; }
        public string DoctorName { get; set; }
        public string VaccineName { get; set; }
        public decimal Price { get; set; }
        public int TottalDay { get; set; }
        public int TottalCow { get; set; }
        public decimal TottalPrice { get; set; }
        public List<Vaccine_Treatment_ReportVM> VTReportVM { get; set; }
    }
}
