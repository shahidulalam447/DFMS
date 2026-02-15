using Firm.Service.Services.Cow_Services;
using FirmWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirmWebApp.Controllers
{
    //[Authorize(Roles = "Hotel,Admin")]
    public class HomeController : Controller
    {
        private readonly ICowService _cowService;
        public HomeController(ICowService _cowService)
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