using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FirmDBContext>
    {
        public FirmDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FirmDBContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=FirmWebAppDB;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true;");
            return new FirmDBContext(optionsBuilder.Options);

        }
    }
}