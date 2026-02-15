using Firm.Infrastructure.Auth;
using Firm.Infrastructure.Data;
using Firm.Service.Services.ExpenseApproval_Services;
using Firm.Service.Services.Breed_Service;
using Firm.Service.Services.Cow_Services;
using Firm.Service.Services.Doctor_Services;
using Firm.Service.Services.FeedCategory_Services;
using Firm.Service.Services.FeedConsumptionBulk_Services;
using Firm.Service.Services.FeedConsumptionCowWise_Services;
using Firm.Service.Services.FeedCurrentStock_Services;
using Firm.Service.Services.FeedEntry_Services;
using Firm.Service.Services.Milk_Services;
using Firm.Service.Services.Report_Services;

using Firm.Service.Services.Employee_Services;
using Firm.Service.Services.SaleableItem_Services;
using Firm.Service.Services.Treatment_Services;
using Firm.Service.Services.Vaccine_Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Firm.Service.Services.Grade_Service;
using Firm.Service.Services.Department_Services;
using Firm.Service.Services.JobInformation_Services;
using Firm.Service.Services.Bank_Services;

namespace Firm.Infrastructure
{
    public static class IocContainerAnother
    {
        public static IServiceCollection AddInfrastructureAnother(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICowService, CowService>();
            services.AddScoped<IBreedService, BreedService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<IVaccineService, VaccineService>();
            services.AddScoped<IMilkService, MilkService>();
            services.AddScoped<IFeedCategoryService, FeedCategoryService>();
            services.AddScoped<IFeedConsumptionBulkService, FeedConsumptionBulkService>();
            services.AddScoped<IFeedConsumptionCowWiseService, FeedConsumptionCowWiseService>();
            services.AddScoped<IFeedEntryService, FeedEntryService>();
            services.AddScoped<IFeedCurrentStockService, FeedCurrentStockService>();
            services.AddScoped<ISaleableItemService, SaleableItemService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IExpenseApprovalService, ExpenseApprovalService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IJobInformationService, JobInformationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IBankService, BankService>();
            services.AddHttpContextAccessor();
            return services;
        }

    }
}
