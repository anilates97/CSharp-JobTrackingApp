using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.DataAccess.Interfaces
{
    public interface IAppUserDal
    {
        List<AppUser> GetirAdminOlmayanlar();
        List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKelime, int aktifSayfa = 1); // toplam sayfayı dışarı fırlatacak. 

        List<DualHelper> GetirEnCokGorevTamamlamisPersoneller();

        List<DualHelper> GetirEnCokGorevdeCalisanPersoneller();
    }
}
