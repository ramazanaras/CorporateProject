using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.Entities;
using NHibernate.Linq;

namespace CorporateProject.Core.DataAccess.Nhibernate
{
    public class NHEntityRepositoryBase<TEntity>:IEntityRepository<TEntity>
    where TEntity:class,IEntity,new()
    {
        public NHibernateHelper _nHibernateHelper;

        public NHEntityRepositoryBase(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public TEntity Add(TEntity entity)
        {
            using (var session=_nHibernateHelper.OpeSession())
            {
                session.Save(entity);
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
           using (var session = _nHibernateHelper.OpeSession())
            {
                session.Delete(entity);
         
            }
        }
        public TEntity Update(TEntity entity)
        {
            using (var session = _nHibernateHelper.OpeSession())
            {
                session.Update(entity);
                return entity;
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var session = _nHibernateHelper.OpeSession())
            {
                return session.Query<TEntity>().SingleOrDefault();
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var session = _nHibernateHelper.OpeSession())
            {
                return filter==null ? session.Query<TEntity>().ToList() : session.Query<TEntity>().Where(filter).ToList();
            }
        }

       
    }
}
