using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyProject.ToDo.Entities.Interfaces;

namespace MyProject.ToDo.Entities.Concrete
{
    public class Gorev : ITablo
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturulmaTarih { get; set; } = DateTime.Now; // günümüzün tarihi neyse o tarihle veriyi oluştur

        public int AciliyetId { get; set; }
        public Aciliyet Aciliyet { get; set; }

        public AppUser AppUser { get; set; }        // böylece bir görevin bir kullanıcı olmak zorunda dedik  1-1 ilişki
        public int? AppUserId { get; set; }  // önce görevleri oluşturup sonra oluşturduğumuz görevi bir kullanıcıya atacağımız için AppUserId yi nullable yaptık ? koyarak.

        public List<Rapor> Raporlar { get; set; }

    }
}
