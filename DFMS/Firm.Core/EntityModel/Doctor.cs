using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Firm.Core.EntityModel
{
    public class Doctor : BaseEntity
    {
        [Display(Name = "Doctor's Name")]
        public string DoctorName { get; set; }
        [Display(Name = "Doctor's Phone")]
        public string DoctorPhone { get; set; }
        [Display(Name = "Doctor's Address")]
        public string DoctorAddress { get; set;}
    }
}
