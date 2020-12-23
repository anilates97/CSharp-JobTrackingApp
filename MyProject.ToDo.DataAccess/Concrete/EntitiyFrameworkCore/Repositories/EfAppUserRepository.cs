using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts;
using MyProject.ToDo.DataAccess.Interfaces;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
    public class EfAppUserRepository : IAppUserDal
    {
        /* 
            select * from AspNetUsers inner join AspNetUserRoles
            on AspNetUsers.Id = AspNetUserRoles.UserId
            inner join AspNetRoles
            on AspNetUserRoles.RoleId = AspNetRoles.Id where AspNetRoles.Name='Member'
         */
        public List<AppUser> GetirAdminOlmayanlar()
        {
            using var context = new TodoContext();

           return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            { // isimsiz bir obje oluşturuyoruz
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
             (resultTable, resultRole) => new
             {
                 user = resultTable.user,
                 userRoles = resultTable.userRole,
                 roles = resultRole
             }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()

             {
                 Id = I.user.Id,
                 Name = I.user.Name,
                 Surname = I.user.Surname,
                 Picture = I.user.Picture,
                 Email = I.user.Email,
                 UserName = I.user.UserName
             }).ToList();
        }

        public List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa, string aranacakKelime, int aktifSayfa=1) // arama sayfası
        {
            using var context = new TodoContext();

            var result =  context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            { // isimsiz bir obje oluşturuyoruz
                user = resultUser,
                userRole = resultUserRole
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
              (resultTable, resultRole) => new
              {
                  user = resultTable.user,
                  userRoles = resultTable.userRole,
                  roles = resultRole
              }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()

              {
                  Id = I.user.Id,
                  Name = I.user.Name,
                  Surname = I.user.Surname,
                  Picture = I.user.Picture,
                  Email = I.user.Email,
                  UserName = I.user.UserName
              });

            toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3); // eğer 7 veri varsa 3. sayfada 1 tane olacağından bunu düzeltmek için önce double a cast edip yuvarladık sonra da çıkan sonucu int e cast ettik


            if (!string.IsNullOrWhiteSpace(aranacakKelime)) // aranacak yer boş değilse
            {
                result.Where(I => I.Name.ToLower().Contains(aranacakKelime.ToLower()) 
                || I.Surname.ToLower().Contains(aranacakKelime.ToLower()));

                toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);
            }

            //pagination

            result = result.Skip((aktifSayfa - 1) * 3).Take(3); // ilerle  -> bir sayfada 3 tane kullanıcı göstermek istediğimizden *3 dedim 3 tane göster ve 3 tane al


            return result.ToList();
        }

        public List<DualHelper> GetirEnCokGorevTamamlamisPersoneller()
        {
            using var context = new TodoContext();
            return context.Gorevler.Include(I => I.AppUser)
                .Where(I => I.Durum)
                .GroupBy(I => I.AppUser.UserName)
                .OrderByDescending(I => I.Count())
                .Take(5)
                .Select(I => new DualHelper
                { 
                    Isim = I.Key,
                    GorevSayisi = I.Count()
                })
                .ToList();

        }

        public List<DualHelper> GetirEnCokGorevdeCalisanPersoneller()
        {
            using var context = new TodoContext();
            return context.Gorevler.Include(I => I.AppUser)
                .Where(I => !I.Durum && I.AppUserId != null)
                .GroupBy(I => I.AppUser.UserName)
                .OrderByDescending(I => I.Count())
                .Take(5)
                .Select(I => new DualHelper
                {
                    Isim = I.Key,
                    GorevSayisi = I.Count()
                })
                .ToList();

        }

    }

    /*
        select AspNetUsers.UserName, count(Gorevler.Id) from AspNetUsers inner join Gorevler on
        AspNetUsers.Id=Gorevler.AppUserId
        where Gorevler.Durum=1 group by AspNetUsers.UserName  
     */

    //class ThreeModel
    //{
    //    public AppUser AppUser { get; set; }
    //    public AppRole AppRole { get; set; }
    //}
}
