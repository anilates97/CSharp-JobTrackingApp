using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.Entities.Concrete;
using MyProject.ToDo.Web.Areas.Admin.Models;

namespace MyProject.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;
        public GorevController(IGorevService gorevService, IAciliyetService aciliyetService)  // dependency injection
        {
            _gorevService = gorevService;
            _aciliyetService = aciliyetService;
        }

        public IActionResult Index()
        {  
            TempData["Active"] = "gorev";
            List<Gorev> gorevler = _gorevService.GetirAciliyetIleTamamlanmayan();
            List<GorevListViewModel> model = new List<GorevListViewModel>();

            foreach (var item in gorevler)
            {
                GorevListViewModel gorevModel = new GorevListViewModel()
                {
                    Id = item.Id,
                    Aciklama = item.Aciklama,
                    Aciliyet = item.Aciliyet,    //artık bu Aciliyet nesnesi dolu çünkü Gorevlerdeki her bir aciliyeti eager loading tarafından dolduruldu
                    AciliyetId = item.AciliyetId,
                    Ad = item.Ad,
                    Durum = item.Durum,
                    OlusturulmaTarih = item.OlusturulmaTarih

                };
                model.Add(gorevModel);
            }
            return View(model);
        }

        public IActionResult EkleGorev()
        {
            TempData["Active"] = "gorev";

            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(),"Id","Tanim"); // EkleGorev.cshtml e atacağımız viewBag i burada oluşturduk. Ve içine SelectList döndürent bir liste tanımladık. Bunlar tek tek sayfada gösterilecek
            return View(new GorevAddViewModel());
        }

        [HttpPost]
        public IActionResult EkleGorev(GorevAddViewModel model)
        {
            if(ModelState.IsValid)
            {
                _gorevService.Kaydet(new Gorev()
                {
                    Aciklama = model.Aciklama,
                    Ad = model.Ad,
                    AciliyetId = model.AciliyetId
                }
                    );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult GuncelleGorev(int id)
        {
           var gorev = _gorevService.GetirIdile(id);
            GorevUpdateViewModel model = new GorevUpdateViewModel()
            {
                Id = gorev.Id,
                Aciklama = gorev.Aciklama,
                AciliyetId = gorev.AciliyetId,
                Ad = gorev.Ad
            };
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim",gorev.AciliyetId);
            return View(model);
        }

        
        [HttpPost]
        public IActionResult GuncelleGorev(GorevUpdateViewModel model)
        {
            TempData["Active"] = "gorev";
            if (ModelState.IsValid)
            {
                _gorevService.Guncelle(new Gorev()
                {
                    Id = model.Id,
                    Aciklama = model.Aciklama,
                    AciliyetId = model.AciliyetId,
                    Ad = model.Ad

                }
                    );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult SilGorev(int id)
        {
            _gorevService.Sil(new Gorev
            {
                Id = id,
            });
            return Json(null); 
        }
    }
}
