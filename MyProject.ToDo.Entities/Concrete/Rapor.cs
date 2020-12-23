using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Interfaces;

namespace MyProject.ToDo.Entities.Concrete
{
    public class Rapor : ITablo
    {
        public int Id { get; set; }
        public string Tanim { get; set; }
        public string Detay { get; set; }

        public Gorev Gorev { get; set; }
        public int GorevId { get; set; }
    }
}
