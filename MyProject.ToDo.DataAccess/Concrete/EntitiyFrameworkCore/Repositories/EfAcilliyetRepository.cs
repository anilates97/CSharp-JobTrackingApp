using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.DataAccess.Interfaces;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
   public class EfAcilliyetRepository: EfGenericRepository<Aciliyet>,IAciliyetDal
    {
    }
}
