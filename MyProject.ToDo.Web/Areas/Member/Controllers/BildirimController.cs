using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.Entities.Concrete;
using MyProject.ToDo.Web.Areas.Admin.Models;

namespace MyProject.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class BildirimController : Controller
    {
        private readonly IBildirimService _bildirimService;
        private readonly UserManager<AppUser> _userManager;

        public BildirimController(IBildirimService bildirimService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _bildirimService = bildirimService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "bildirim";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bildirimler = _bildirimService.GetirOkunmayanlar(user.Id);
            List<BildirimListViewModel> models = new List<BildirimListViewModel>();

            foreach (var bildirim in bildirimler)
            {
                BildirimListViewModel model = new BildirimListViewModel
                {
                    Id = bildirim.Id,
                    Aciklama = bildirim.Aciklama
                    
                };
                models.Add(model);
            }

            return View(models);
        }


        [HttpPost]
        public IActionResult Index(int id)
        {
            TempData["Active"] = "bildirim";
            var guncellenecekBildirim = _bildirimService.GetirIdile(id);
            guncellenecekBildirim.Durum = true;
            _bildirimService.Guncelle(guncellenecekBildirim);
            return RedirectToAction("Index");
        }
    }
}
