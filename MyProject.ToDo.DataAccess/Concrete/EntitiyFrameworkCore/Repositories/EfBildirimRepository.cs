using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts;
using MyProject.ToDo.DataAccess.Interfaces;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimDal
    {
        public List<Bildirim> GetirOkunmayanlar(int AppUserId)
        {
            using var context = new TodoContext();
            return context.Bildirimler.Where(I => I.AppUserId == AppUserId && !I.Durum).ToList();
        }

        public int GetirOkunmayanSayisiileAppUserId(int AppUserId)
        {
            using var context = new TodoContext();
            return context.Bildirimler.Count(I => I.AppUserId == AppUserId && !I.Durum);
        }
    }
}
