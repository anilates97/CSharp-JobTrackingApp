using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly IGorevService _gorevService;
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRaporService _raporService;

        public HomeController(IGorevService gorevService, IBildirimService bildirimService, UserManager<AppUser> userManager, IRaporService raporService)
        {
            _raporService = raporService;
            _userManager = userManager;
            _bildirimService = bildirimService;
            _gorevService = gorevService;
        }


        /* 
         * Atanmayı Bekleyen Görev Sayısı
         * Tamamlanmış Görev Sayısı
         * Okunmamış Bildirim Sayısı
         * Toplam Yazılmış Rapor Sayısı

         */
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "anasayfa";

            // Atanmayı Bekleyen Görev Sayısı
            ViewBag.AtanmayiBekleyenGorevSayisi = _gorevService.GetirAtanmayıBekleyenGorevSayisi();


            // Tamamlanmış Görev Sayısı
            ViewBag.TamamlanmisGorevSayisi = _gorevService.GetirGorevTamamlanmis();

            // Okunmamış Bildirim Sayısı

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.OkunmamisBildirimSayisi = _bildirimService.GetirOkunmayanSayisiileAppUserId(user.Id);


            // Toplam Yazılmış Rapor Sayısı
            ViewBag.ToplamRaporSayisi = _raporService.GetirRaporSayisi();
            return View();
        }
    }
}
