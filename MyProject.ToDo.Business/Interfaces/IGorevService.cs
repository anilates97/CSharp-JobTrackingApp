using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Business.Interfaces
{
   public interface IGorevService : IGenericService<Gorev>
    {
        List<Gorev> GetirAciliyetIleTamamlanmayan();
        List<Gorev> GetirTumTablolarla();
        List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter); // expression geçtik linq sorgusu yazmak istediğimiz için

        List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa=1);
        Gorev GetirAciliyetileId(int id);

        List<Gorev> GetirileAppUserId(int appUserId);
        Gorev GetirRaporlarileId(int id);

        int GetirGorevSayisiTamamlananileAppUserId(int id);
        int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int id);

        int GetirAtanmayıBekleyenGorevSayisi();

        int GetirGorevTamamlanmis();



    }
}
