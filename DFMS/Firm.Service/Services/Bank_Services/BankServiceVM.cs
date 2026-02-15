using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Bank_Services
{
    public class BankServiceVM
    {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string BankBranch { get; set; }
        public string CreatedBy { get; set; }
        public string DisburseMethod { get; set; }
        public string BankAccount { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }
    }
}
