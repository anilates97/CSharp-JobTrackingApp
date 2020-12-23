using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.Entities.Concrete;
using MyProject.ToDo.Web.Areas.Admin.Models;

namespace MyProject.ToDo.Web.ViewComponents
{
    public class Wrapper : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBildirimService _bildirimService;
        public Wrapper(UserManager<AppUser> userManager, IBildirimService bildirimService)
        {
            _bildirimService = bildirimService;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            AppUserListViewModel model = new AppUserListViewModel();

            model.Id = user.Id;
            model.Name = user.Name;
            model.Picture = user.Picture;
            model.SurName = user.Surname;
            model.Email = user.Email;

            // Bildirim *******
            var bildirimler = _bildirimService.GetirOkunmayanlar(user.Id).Count;
            ViewBag.BildirimSayisi = bildirimler;


            var roles = _userManager.GetRolesAsync(user).Result;

            if(roles.Contains("Admin"))
            {
                return View(model);
            }
            return View("Member", model);
            
        }
    }
}
