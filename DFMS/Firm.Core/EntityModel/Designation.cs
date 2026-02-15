using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Core.EntityModel
{
    public class Designation:BaseEntity
    {
        public string DesignationName { get; set; }
        [Required, Range(0, Double.MaxValue, ErrorMessage = "Salary Amount must be greater than 0")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public string ErrorMessage { get; set; }
    }

}
