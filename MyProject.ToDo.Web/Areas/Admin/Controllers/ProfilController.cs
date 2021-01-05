using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.ToDo.Entities.Concrete;
using MyProject.ToDo.Web.Areas.Admin.Models;

namespace MyProject.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProfilController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public ProfilController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "profil";
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserListViewModel model = new AppUserListViewModel();
            model.Id = appUser.Id;
            model.Name = appUser.Name;
            model.SurName = appUser.Surname;
            model.Picture = appUser.Picture;
            model.Email = appUser.Email;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserListViewModel model, IFormFile resim) // resim ekleme için IFormFile kullandık
        {
            // resim ekleme
            if (ModelState.IsValid)
            {
                var guncellenecekKullanici = _userManager.Users.FirstOrDefault(I => I.Id == model.Id);
                if (resim != null)  // veritabanını gereksiz yere yormamak amacıyla bu kontrolü yapıyoruz
                {
                    string uzanti = Path.GetExtension(resim.FileName);  
                    string resimAd = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory()
                        , "wwwroot/img/" + resimAd);
                    using var stream = new FileStream(path, FileMode.Create);
                    await resim.CopyToAsync(stream);

                    guncellenecekKullanici.Picture = resimAd;  
                }

                guncellenecekKullanici.Name = model.Name;
                guncellenecekKullanici.Surname = model.SurName;
                guncellenecekKullanici.Email = model.Email;

                var result = await _userManager.UpdateAsync(guncellenecekKullanici);

                if(result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işleminiz başarı ile gerçekleşti";
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
    }
}
