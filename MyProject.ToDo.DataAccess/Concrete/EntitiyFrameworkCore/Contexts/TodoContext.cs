using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Mapping;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts
{
   public class TodoContext : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //OnConfiguring ve OnModelCreating metodlarını base a tekrar göndermemiz gerekiyor ki identity nin içinde bu yorumlansın
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-A91PHS3; database=IsDb; integrated security=true");
            base.OnConfiguring(optionsBuilder); //base
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GorevMap());
            modelBuilder.ApplyConfiguration(new AciliyetMap());
            modelBuilder.ApplyConfiguration(new RaporMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());

            base.OnModelCreating(modelBuilder); //base

        }

       
        public DbSet<Gorev> Gorevler { get; set; }

        public DbSet<Aciliyet> Aciliyetler { get; set; }

        public DbSet<Rapor> Raporlar { get; set; }

        public DbSet<Bildirim> Bildirimler { get; set; }
    }
}
