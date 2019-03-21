using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.Entities;

namespace CorporateProject.Entities.Concrete
{
    //veritabanı sınıfı
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
