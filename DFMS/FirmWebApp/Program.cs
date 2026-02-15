using Firm.Infrastructure;
using Firm.Infrastructure.Data;
using Firm.Service.Services.Employee_Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationCookie(
                           options =>
                           {
                               options.LoginPath = new PathString("/User_Login/Login");
                               options.AccessDeniedPath = new PathString("/User_Login/AccessDenied");
                               options.LogoutPath = new PathString("/User_Login/Logout");
                           });
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});



builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureAnother(builder.Configuration);
//builder.Services.AddServiceAnotherModel(builder.Configuration);
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<FirmDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("appCon")));
builder.Services.AddTransient<EmployeeService, EmployeeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpsRedirection();
//app.UseCors("_myAllowSpecificOrigins");
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
               name: "default",
               pattern: "{controller=User_Login}/{action=Login}/{id?}");

app.Run();
