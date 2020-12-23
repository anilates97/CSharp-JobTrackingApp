using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Business.Interfaces
{
    public interface IAppUserService
    {
        List<AppUser> GetirAdminOlmayanlar();
        List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKelime, int aktifSayfa = 1);

        List<DualHelper> GetirEnCokGorevTamamlamisPersoneller();

        List<DualHelper> GetirEnCokGorevdeCalisanPersoneller();
    }
}
