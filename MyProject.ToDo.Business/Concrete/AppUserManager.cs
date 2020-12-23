using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.DataAccess.Interfaces;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
       private readonly IAppUserDal _userDal;

        public AppUserManager(IAppUserDal userDal)
        {
            _userDal = userDal;
        }
        public List<AppUser> GetirAdminOlmayanlar()
        {
            return _userDal.GetirAdminOlmayanlar();
        }

        public List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKelime, int aktifSayfa)
        {
            return _userDal.GetirAdminOlmayanlar(out toplamSayfa, aranacakKelime, aktifSayfa); // out ToplamSayfa -> toplam sayfa değerine yukarıya atacaktır
        }

        public List<DualHelper> GetirEnCokGorevdeCalisanPersoneller()
        {
            return _userDal.GetirEnCokGorevdeCalisanPersoneller();
        }

        public List<DualHelper> GetirEnCokGorevTamamlamisPersoneller()
        {
            return _userDal.GetirEnCokGorevTamamlamisPersoneller();
        }
    }
}
