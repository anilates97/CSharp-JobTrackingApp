using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.DataAccess.Interfaces
{
    public interface IGorevDal: IGenericDal<Gorev>
    {
        List<Gorev> GetirAciliyetIleTamamlanmayan();
        List<Gorev> GetirTumTablolarla();
        List<Gorev> GetirTumTablolarla(Expression<Func<Gorev,bool>> filter); // expression geçtik linq sorgusu yazmak istediğimiz için
        Gorev GetirAciliyetileId(int id);
        List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa);

        List<Gorev> GetirileAppUserId(int appUserId);
        Gorev GetirRaporlarileId(int id);

        int GetirGorevSayisiTamamlananileAppUserId(int id);
        int GetirGorevSayisiTamamlanmasiGerekenileAppUserId(int id);

        int GetirAtanmayıBekleyenGorevSayisi();

        int GetirGorevTamamlanmis();
    }
}
