using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.DataAccess.Nhibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace CorporateProject.DataAccess.Concrete.NHibernate.Helpers
{
   public  class SqlServerHelper:NHibernateHelper
    {
        protected override ISessionFactory InitializeFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c =>
                    c.FromConnectionStringWithKey("Northwind_Name"))) //connection stringteki ismi verdik
                .Mappings(t => t.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())).BuildSessionFactory();
        }
    }
}


//manage nugettan fluentnhibernate yükle