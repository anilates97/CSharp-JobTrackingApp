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
using MyProject.ToDo.Web.Models;

namespace MyProject.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;

        private readonly IBildirimService _bildirimService;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService
            , UserManager<AppUser> userManager, IDosyaService dosyaService, IBildirimService bildirimService)
        {
            _bildirimService = bildirimService;
            _dosyaService = dosyaService; // dependency injection
            _userManager = userManager;
            _gorevService = gorevService;
            _appUserService = appUserService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = "isemri";
            //var model = _appUserService.GetirAdminOlmayanlar();
            List<Gorev> gorevler = _gorevService.GetirTumTablolarla();
            List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();

            foreach (var item in gorevler)
            {
                GorevListAllViewModel model = new GorevListAllViewModel();
                model.Id = item.Id;
                model.Aciklama = item.Aciklama;
                model.Aciliyet = item.Aciliyet;
                model.Ad = item.Ad;
                model.AppUser = item.AppUser;
                model.OlusturulmaTarih = item.OlusturulmaTarih;
                model.Raporlar = item.Raporlar;
                models.Add(model);
            }

            return View(models);
        }


        public IActionResult Detaylandir(int id)
        {
            TempData["Active"] = "isemri";
            var gorev = _gorevService.GetirRaporlarileId(id);
            GorevListAllViewModel model = new GorevListAllViewModel();
            model.Id = gorev.Id;
            model.Raporlar = gorev.Raporlar;    
            model.Ad = gorev.Ad;
            model.Aciklama = gorev.Aciklama;
            model.AppUser = gorev.AppUser;
            return View(model);
        }

        public IActionResult GetirExcel(int id)
        {
            
          return File(_dosyaService.AktarExcel(_gorevService.GetirRaporlarileId(id).Raporlar)
              , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
              ,Guid.NewGuid()+".xlsx");
        }

        public IActionResult GetirPdf(int id)
        {
            var path = _dosyaService.AktarPdf(_gorevService.GetirRaporlarileId(id).Raporlar);
            return File(path, "application/pdf", Guid.NewGuid() + ".pdf");
        }

        //localhost/?s=anil
        public IActionResult AtaPersonel(int id, string s, int sayfa = 1)
        {
            TempData["active"] = "isemri";

            ViewBag.AktifSayfa = sayfa; // aktif sayfayı viewbag ile aldık
                                        //ViewBag.ToplamSayfa = (int)Math.Ceiling((double) // eğer 7 veri varsa 3. sayfada 1 tane olacağından bunu düzeltmek için önce double a cast edip yuvarladık sonra da çıkan sonucu int e cast ettik
                                        //    _appUserService.GetirAdminOlmayanlar().Count / 3); // parametresiz olanı getiriyoruz çünkü toplam admin olmayan kullanıcı sayısını getiriyor // 1 sayfa 3 personel göstermek istediğimizden dolayı bunu 3 e böldük


            ViewBag.Aranan = s;
            int toplamSayfa;
            var gorev = _gorevService.GetirAciliyetileId(id);




            //Görevlerin getirilmesi 
            GorevListViewModel gorevModel = new GorevListViewModel();
            gorevModel.Id = gorev.Id;
            gorevModel.Ad = gorev.Ad;
            gorevModel.Aciklama = gorev.Aciklama;
            gorevModel.Aciliyet = gorev.Aciliyet;
            gorevModel.OlusturulmaTarih = gorev.OlusturulmaTarih;


            //Personelin getirilmesi
            var personeller = _appUserService.GetirAdminOlmayanlar(out toplamSayfa, s, sayfa);

            ViewBag.ToplamSayfa = toplamSayfa;

            List<AppUserListViewModel> appUserListModel = new List<AppUserListViewModel>();
            foreach (var item in personeller)
            {
                AppUserListViewModel model = new AppUserListViewModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.SurName = item.Surname;
                model.Email = item.Email;
                model.Picture = item.Picture;
                appUserListModel.Add(model);
            }

            ViewBag.Personeller = appUserListModel;

            return View(gorevModel);
        }

        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirViewModel model)
        {
            var guncellenecekGorev = _gorevService.GetirIdile(model.GorevId);
            guncellenecekGorev.AppUserId = model.PersonelId;
            _gorevService.Guncelle(guncellenecekGorev);

            // Bildirim *********
            _bildirimService.Kaydet(new Bildirim
            {
                AppUserId = model.PersonelId,
                Aciklama = $"{guncellenecekGorev.Ad} adlı iş için görevlendirildiniz."
            });
            // Bildirim ***

            return RedirectToAction("Index");
        }

        public IActionResult GorevlendirPersonel(PersonelGorevlendirViewModel model)
        {
            TempData["active"] = "isemri";
            var user = _userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId);

            var gorev = _gorevService.GetirAciliyetileId(model.GorevId);

            //USER
            AppUserListViewModel userModel = new AppUserListViewModel();
            userModel.Id = user.Id;
            userModel.Name = user.Name;
            userModel.Picture = user.Picture;
            userModel.SurName = user.Surname;
            userModel.Email = user.Email;

            //GOREV
            GorevListViewModel gorevModel = new GorevListViewModel();
            gorevModel.Id = gorev.Id;
            gorevModel.Aciklama = gorev.Aciklama;
            gorevModel.Aciliyet = gorev.Aciliyet;
            gorevModel.Ad = gorev.Ad;

            PersonelGorevlendirListViewModel personelGorevlendirModel = new PersonelGorevlendirListViewModel();

            //AppUserListView model ve PersonelGorevlendirListView modelin içine yeni oluşturduğumuz modelleri atadık
            personelGorevlendirModel.AppUser = userModel;
            personelGorevlendirModel.Gorev = gorevModel;
            return View(personelGorevlendirModel);
        }
    }
}
