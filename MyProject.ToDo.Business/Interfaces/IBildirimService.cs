using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.Business.Interfaces
{
    public interface IBildirimService : IGenericService<Bildirim>
    {
        List<Bildirim> GetirOkunmayanlar(int AppUserId);
        int GetirOkunmayanSayisiileAppUserId(int AppUserId);



    }
}
