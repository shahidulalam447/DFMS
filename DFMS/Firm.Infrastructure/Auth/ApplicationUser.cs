using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Infrastructure.Auth
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public int UserType { get; set; }
        public long HotelId { get; set; }
        public string PreFix { get; set; }
        public DateTime CrateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
