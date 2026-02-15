using Firm.Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Department_Services
{
    public class DepartmentServiceVM
    {
        public long Id { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } 
        public string ErrorMessage { get; set; }

    }
}
