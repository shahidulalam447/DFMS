using Firm.Service.Services.Cow_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FirmWebApp.Controllers
{
    [Authorize(Roles = "Hotel,Admin")]
    public class AdminController : Controller
    {
        private readonly ICowService _cowService;
        public AdminController(ICowService _cowService)
        {
            this._cowService = _cowService;
        }
        public async Task<IActionResult> Index()
        {
            var obj = await _cowService.CowSummary30Days();
            return View(obj);
        }
    }
}
