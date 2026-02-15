using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services
{
    public class BaseVM
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }
    }
}
