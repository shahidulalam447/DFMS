using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Firm.Utility.Miscellaneous.Enum;

namespace Firm.Service.Services.ExpenseApproval_Services
{
   public class ExpenseApprovalServiceVM
    {
        public long Id { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string RefNo { get; set; }
        public PaymentMode? PaymentModeId { get; set; }
        public ExpenseType? ExpenseItemId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
