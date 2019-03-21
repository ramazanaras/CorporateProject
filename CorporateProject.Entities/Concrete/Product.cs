using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.Entities;

namespace CorporateProject.Entities.Concrete
{
    //veritabanı sınıfı
    public class Product : IEntity
    {
        //nhibernatede sıkıntı çıkmasın diye bütün propertilerin başına virtual koy
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string QuantityPerUnit { get; set; }
        public virtual decimal UnitPrice { get; set; }
    }
}
