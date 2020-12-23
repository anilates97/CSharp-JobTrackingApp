using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Business.Interfaces
{
   public interface IRaporService : IGenericService<Rapor>
    {
        Rapor GetirGorevileId(int id);

        int GetirRaporSayisiileAppUserId(int id);

        int GetirRaporSayisi();


    }
}
