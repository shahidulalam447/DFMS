using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Service.Services.Breed_Service
{
    public class BreedServiceViewModel
    {
        public long BreedId { get; set; } = 0;
        public string BreedName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
