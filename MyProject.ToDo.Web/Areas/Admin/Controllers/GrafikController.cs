using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MyProject.ToDo.Business.Interfaces;

namespace MyProject.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GrafikController : Controller
    {
        private readonly IAppUserService _appUserService;


        public GrafikController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        /*
         * En çok görev tamamlamış 5 personel
         * En çok görev almış 5 personel. 
         
         */
        public IActionResult Index()
        {
            TempData["Active"] = "grafik";
            return View();
        }

        public IActionResult EnCokTamamlayan()
        {
            var jsonString = JsonConvert.SerializeObject(_appUserService.GetirEnCokGorevTamamlamisPersoneller());
            return Json(jsonString);
        }

        public IActionResult EnCokCalisan()
        {
            var jsonString = JsonConvert.SerializeObject(_appUserService.GetirEnCokGorevdeCalisanPersoneller());
            return Json(jsonString);
        }
    }
}
