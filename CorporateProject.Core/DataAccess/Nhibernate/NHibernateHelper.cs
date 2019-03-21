using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace CorporateProject.Core.DataAccess.Nhibernate
{
    //manage nugettan nhibernate yükle
    public abstract class NHibernateHelper:IDisposable
    {
        private static ISessionFactory _sessionFactory;

        public virtual ISessionFactory SessionFactory
        {
            get { return _sessionFactory??(_sessionFactory=InitializeFactory());}
        }

        protected abstract ISessionFactory InitializeFactory();

        public virtual ISession OpeSession()
        {
            return SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
