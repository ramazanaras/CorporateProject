using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.Entities;
using NHibernate.Linq;

namespace CorporateProject.Core.DataAccess.Nhibernate
{
  public   class NhQueryableRepository<T>:IQueryableRepository<T> where T:class,IEntity,new()
  {
      private NHibernateHelper _nHibernateHelper;
      private IQueryable<T> _entities;

      public NhQueryableRepository(NHibernateHelper nHibernateHelper)
      {
          _nHibernateHelper = nHibernateHelper;
      }

      public IQueryable<T> Table => this._entities;


      public virtual IQueryable<T> Entities
      {
          get { return _entities ?? (_entities = _nHibernateHelper.OpeSession().Query<T>()); }
      }

  }
}
