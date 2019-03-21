using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.Entities;

namespace CorporateProject.Entities.Concrete
{
    //veritabanı sınıfı
    public class Category:IEntity
    {
        //nhibernatede sıkıntı çıkmasın diye bütün propertilerin başına virtual koy
        public virtual int CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
    }
}
