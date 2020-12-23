using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts;
using MyProject.ToDo.DataAccess.Interfaces;
using MyProject.ToDo.Entities.Interfaces;

namespace MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
    public class EfGenericRepository<Tablo> : IGenericDal<Tablo> where Tablo : class, ITablo, new()
    {
        public List<Tablo> GetirHepsi()
        {
            using var context = new TodoContext();
            return context.Set<Tablo>().ToList();         // ->  context.Calismalar = context.Set<Calisma>();   buna göre hepsinde tek tek değişiklik yaptık
        }

        public Tablo GetirIdile(int id)
        {
            using var context = new TodoContext();
            return context.Set<Tablo>().Find(id);
        }

        // update Calisma set kolon1 = deger1, kolon2 = deger2
        public void Guncelle(Tablo tablo)
        {
            using var context = new TodoContext();
            //context.Entry(tablo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;  // entityframework a güncellenebilir bir entry olduğunu söylüyoruz
            context.Set<Tablo>().Update(tablo);
            context.SaveChanges();
        }

        public void Kaydet(Tablo tablo)
        {
            using var context = new TodoContext();
            context.Set<Tablo>().Add(tablo);
            context.SaveChanges();
        }

        public void Sil(Tablo tablo)
        {
            using var context = new TodoContext();
            context.Set<Tablo>().Remove(tablo);
            context.SaveChanges();
        }
    }
}
