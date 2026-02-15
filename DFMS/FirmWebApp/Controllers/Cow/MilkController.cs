using Firm.Service.Services.Cow_Services;
using Firm.Service.Services.Milk_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirmWebApp.Controllers.Cow
{
    public class MilkController : Controller
    {
        public readonly ICowService _cowService;
        public readonly IMilkService _milkService;
        public MilkController(ICowService cowService, IMilkService milkService)
        {
            _cowService = cowService;
            _milkService = milkService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.cowlist = new SelectList((await _cowService.GetAll()).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            var data = await _milkService.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            var cowList = await _cowService.GetAll();
            ViewBag.cowlist = new SelectList((cowList).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            return View();
        }


        [HttpGet]    
        
        public async Task<IActionResult> Create(MilkServiceViewModel model)
        {
            var cowModel = new MilkServiceViewModel();
            try
            {
                cowModel.Date = model.Date;
                cowModel.ShadeNo=model.ShadeNo;
                cowModel.ShiftVal = model.ShiftVal;
                cowModel.milkServiceVmList=await  _milkService.MilkingCowList(model);
                return View(cowModel);
            }
            catch
            {
                return View(cowModel);

            }
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateMilk(MilkServiceViewModel model)
        
        
        
        {
            try
            {
                var result = await _milkService.AddNewMilk(model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding cow.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var cowList = await _cowService.GetAll();

            ViewBag.cowlist = new SelectList((cowList).Select(s => new { Id = s.Id, Name = s.TagId }), "Id", "Name");
            var obj = await _milkService.GetById(id);
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MilkServiceViewModel model)
        {
            try
            {
                await _milkService.UpdateMilk(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding cow.");
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(long id)
        {
            var obj = await _milkService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
