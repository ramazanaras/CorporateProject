using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Entities.Concrete;
using FluentNHibernate.Mapping;

namespace CorporateProject.DataAccess.Concrete.NHibernate.Mappings
{
    //nhibernate için mapping işlemi
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
                Table(@"Categories");
                LazyLoad();
                Id(x => x.CategoryId).Column("CategoryID");
            
                Map(x => x.CategoryName).Column("CategoryName");
             
        }


    }
}
