using Firm.Core.EntityModel;
using Firm.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Firm.Infrastructure.Data
{
    public class FirmDBContext : IdentityDbContext<ApplicationUser>
    {
        public FirmDBContext(DbContextOptions<FirmDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            FixedData.SeedData(builder);
            base.OnModelCreating(builder);
        }

        public object FindByCondition(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
        public virtual DbSet<Cow> Cows { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
        public virtual DbSet<Vaccine> Vaccines { get; set; }
        public virtual DbSet<MilkMonitor> MilkMonitors { get; set; }
        public virtual DbSet<FeedCategory> FeedCategories { get; set; }
        public virtual DbSet<FeedEntry> FeedEntries { get; set; }
        public virtual DbSet<FeedCurrentStock> FeedCurrentStocks { get; set; }
        public virtual DbSet<FeedConsumptionBulk> FeedConsumptionBulks { get; set; }
        public virtual DbSet<FeedConsumptionCowWise> FeedConsumptionCowWises { get; set; }
        public virtual DbSet<SaleableItem> SaleableItems { get; set; }
        public virtual DbSet<ExpenseApproval> ExpenseApproval { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<JobInformation> JobInformation { get; set; }
        public virtual DbSet<BankModel> BankModels { get; set; }
    }
}
