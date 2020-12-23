using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Interfaces;

namespace MyProject.ToDo.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, ITablo  // primary key int belirledik
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Picture { get; set; } = "default.png";

        public List<Bildirim> Bildirimler { get; set; }
        public List<Gorev> Gorevler { get; set; }     // böylece bir kullanıcının birden fazla görevi olabilir dedik  1-n ilişkisi
    }
}
