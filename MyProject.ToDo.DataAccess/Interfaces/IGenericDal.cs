using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Interfaces;

namespace MyProject.ToDo.DataAccess.Interfaces
{
   public interface IGenericDal<Tablo> where Tablo:class,ITablo,new() // IGenericDal bizden bir tablo bekleyecek bu tablo ITablodan kalıtsal yollarla geçmiş olmak zorunda,class olmak zorunda, new lenebilir olmak zorunda
    {
        void Kaydet(Tablo tablo);
        void Sil(Tablo tablo);

        void Guncelle(Tablo tablo);
        Tablo GetirIdile(int id);
        List<Tablo> GetirHepsi();
    }
}
