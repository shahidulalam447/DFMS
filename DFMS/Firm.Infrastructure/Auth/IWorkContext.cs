using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Infrastructure.Auth
{
    public interface IWorkContext
    {
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<ApplicationUser> CurrentUserAsync();
        Task<bool> IsUserSignedIn();
    }
}
