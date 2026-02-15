using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Core.EntityModel
{
    public class Vaccine : BaseEntity
    {
        public DateTime VaccineDate { get; set; }
        public long CowId { get; set; } = 0;
        public long DoctorId { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int DoseNo { get; set; }
        public string Details { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
