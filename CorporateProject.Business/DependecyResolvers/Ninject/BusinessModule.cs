using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Business.Abstract;
using CorporateProject.Business.Concrete.Managers;
using CorporateProject.Core.DataAccess;
using CorporateProject.Core.DataAccess.EntityFramework;
using CorporateProject.Core.DataAccess.Nhibernate;
using CorporateProject.DataAccess;
using CorporateProject.DataAccess.Abstact;
using CorporateProject.DataAccess.Concrete.EntityFramework;
using CorporateProject.DataAccess.Concrete.NHibernate;
using CorporateProject.DataAccess.Concrete.NHibernate.Helpers;
using Ninject.Modules;

//Business katmanına manage nugettan Ninjecti yüklüyoruz.
namespace CorporateProject.Business.DependecyResolvers.Ninject
{
    public class BusinessModule:NinjectModule
    {
        public override void Load()
        {
            //birisi senden constructorda IProductService isterse ona ProductManager ver.
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
          //  Bind<IProductDal>().To<NhProductDal>().InSingletonScope();//ilerde Nhibernate'e geçiş yaparsak bunu kullanabailiriz.
            Bind<IProductDal>().To<EfProductDal>(); //ProductManagerı new lemeye çalışırken IProductDal'a ihtiyacı olucak.onada EfProductDal'ı vercez

            //birisi senden constructorda IQueryableRepository isterse ona EfQueryableRepository ver.
            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));
            //birisi senden constructorda DbContext isterse ona NorthwindContext ver.Çünkü EfQueryableRepository constructorda DbContext istiyor
            Bind<DbContext>().To<NorthwindContext>();


            //birisi senden constructorda NHibernateHelper isterse ona SqlServerHelper ver.(NHEntityRepositoryBase classında constructorda )
            Bind<NHibernateHelper>().To<SqlServerHelper>();


            //birisi senden constructorda IUserService ona UserManager ver
            Bind<IUserService>().To<UserManager>();

            //UserManager sınıfının constructorıda IUserDal> istiyor onada EfUserDal veriyoruz
            Bind<IUserDal>().To<EfUserDal>();


        }
    }
}

/*
NOT:

    SOLID 'İN D HARFİ hiçbir katmanda başka bir katmanı newleyemezsin diyor.Bu yüzden dependecy injection mekanizmasını kullanmamız gerekiyor.Hiçbir şekilde manager ve veri erişim katmanlarına bağımlı olmamamız lazım

    Yani dependecy injection faydaları;
    Manager(Business) katmanında ben Entityframework veya Nhibernate ile çalışabiliriz.Bağımlılık yok.
    Arayüz katmanında BLL(Business ) veya Api üzeriden veya WCF servisi üzerinde çalışmaya imkan verir.Bağımlılığı ortadan kaldırıyoruz.


 */