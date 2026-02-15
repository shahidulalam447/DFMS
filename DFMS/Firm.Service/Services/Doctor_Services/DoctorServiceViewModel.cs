using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Firm.Service.Services.Doctor_Services
{
    public class DoctorServiceViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Doctor's Name")]
        public string DoctorName { get; set; }
        [Display(Name = "Doctor's Phone")]
        public string DoctorPhone { get; set; }
        [Display(Name = "Doctor's Address")]
        public string DoctorAddress { get; set; }
        public bool IsActive { get; set; } = true;
        public string ErrorMessage { get; set; }
    }
}
