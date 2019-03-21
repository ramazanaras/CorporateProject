using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorporateProject.Business.Concrete.Managers;
using CorporateProject.DataAccess.Concrete.EntityFramework;
using CorporateProject.DataAccess.Concrete.NHibernate;
using CorporateProject.DataAccess.Concrete.NHibernate.Helpers;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        /*ENTİTY FRAMEWORK TESTİ İÇİN */
        // EfProductDal _efProductDal=new EfProductDal();
        //public ActionResult Index()
        //{
        //    var list = _efProductDal.GetList();
        //    return View();

   



        /*NHIBERNATE   TESTİ İÇİN */
        //NhProductDal _nHproductDal=new NhProductDal(new SqlServerHelper());
        //public ActionResult Index()
        //{
        //    var list = _nHproductDal.GetList();
        //    return View();
        //}

  

         ProductManager _manager=new ProductManager(new EfProductDal());
        public ActionResult Index()
        {
            var list = _manager.Add(new Product());
            return View();
        }

    }
}


/*ENTİTY FRAMEWORK TESTİ :  HATA VE ÇÖZÜMÜ
 *No Entity Framework provider found for the ADO.NET provider with invariant name 'System.Data.SqlClient'. Make sure the provider is registered in the 'entityFramework' section of the application config file. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
 MvcWebUI katmanına manage nugettan entity framework yükle
 */

/*webconfige connectionstring eklemeyi unutma
 *
 *  <connectionStrings>
    <add name="Northwind_Name" connectionString="data source=.;initial catalog=NORTHWND;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
  </connectionStrings>
 *
 *
 *
 */




/*NHIBERNATE TESTI :HATA VEV ÇÖZÜMÜ
 *HATA :
 * The following types may not be used as proxies:
CorporateProject.Entities.Concrete.Product: method get_ProductId should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method set_ProductId should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method get_ProductName should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method set_ProductName should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method get_CategoryId should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method set_CategoryId should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method get_QuantityPerUnit should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method set_QuantityPerUnit should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method get_UnitPrice should be 'public/protected virtual' or 'protected internal virtual'
CorporateProject.Entities.Concrete.Product: method set_UnitPrice should be 'public/protected virtual' or 'protected internal virtual'
 *
 *
 *ÇÖZÜMÜ nesnelerdeki propertilerin başına virtual koy
 *
 *    public class Product : IEntity
    {
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string QuantityPerUnit { get; set; }
        public virtual decimal UnitPrice { get; set; }
    }
 *
 */
