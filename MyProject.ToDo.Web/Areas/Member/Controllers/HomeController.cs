using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IRaporService _raporService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;

        private readonly IGorevService _gorevService;
        public HomeController(IRaporService raporService, UserManager<AppUser> userManager, IGorevService gorevService, IBildirimService bildirimService)
        {
            _bildirimService = bildirimService;
            _gorevService = gorevService;
            _userManager = userManager;
            _raporService = raporService;
        }
        /*
         * İlgili Kullanıcının yazdığı rapor sayısı
         * İlgili Kullanıcının tamamladığı görev sayısı
         * İlgili Kullanıcının okumadığı bildirim sayısı
         * İlgili Kullanıcının tamamlaması gereken görev sayısı
        */


        public async Task<IActionResult> Index()
        {
            // İlgili Kullanıcının yazdığı rapor sayısı
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["Active"] = "anasayfa";
            ViewBag.RaporSayisi = _raporService.GetirRaporSayisiileAppUserId(user.Id);

            // İlgili Kullanıcının tamamladığı görev sayısı
            ViewBag.TamamlananGorevSayisi = _gorevService.GetirGorevSayisiTamamlananileAppUserId(user.Id);

            // İlgili Kullanıcının okumadığı bildirim sayısı
            ViewBag.OkunmamisBildirimSayisi = _bildirimService.GetirOkunmayanSayisiileAppUserId(user.Id);

            // İlgili Kullanıcının okumadığı bildirim sayısı
            ViewBag.TamamlanmasiGerekenGorevSayisi = _gorevService.GetirGorevSayisiTamamlanmasiGerekenileAppUserId(user.Id);

            return View();

            
          
        }
    }
}
