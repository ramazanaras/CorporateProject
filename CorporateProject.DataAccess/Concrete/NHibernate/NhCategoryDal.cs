using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.DataAccess.Nhibernate;
using CorporateProject.DataAccess.Abstact;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.DataAccess.Concrete.NHibernate
{
   public  class NhCategoryDal:NHEntityRepositoryBase<Category>,ICategoryDal
    {
        public NhCategoryDal(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
        }
    }
}
