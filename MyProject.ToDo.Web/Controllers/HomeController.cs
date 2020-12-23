using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.Entities.Concrete;
using MyProject.ToDo.Web.Models;

namespace MyProject.ToDo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(IGorevService gorevService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)  // dependency injection -> Bu yapıyla neyi görünce neyi örnekleneğini belirtebiliyoruz startupta.
        {
            _signInManager = signInManager;  
            _userManager = userManager; // nesne örneğini atadık
            _gorevService = gorevService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserSignInModel model)
        {
            if(ModelState.IsValid)
            {
               var user =  await _userManager.FindByNameAsync(model.UserName);
                if(user != null)
                {
                    var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    if(identityResult.Succeeded)
                    {
                        var roller = await  _userManager.GetRolesAsync(user);  // kullanıcının rollerini getirdik
                        if (roller.Contains("Admin"))
                        {
                            return RedirectToAction("Index","Home", new { area = "Admin" });  // eğer rollerde admin içeriyorsa admin areasına yönlendirdik
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });// eğer rollerde member içeriyorsa member areasına yönlendirdik
                        }

                    }
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }
            return View("Index",model);
        }

        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };
                var result = await _userManager.CreateAsync(user,
                    model.Password);

                if (result.Succeeded)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "Member"); // kayıt olurken rolünü tanımlıyoruz

                    if (addRoleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    foreach (var item in addRoleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);   // hataları model state ekliyoruz
                }
            }


            return View(model);
        }

        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
