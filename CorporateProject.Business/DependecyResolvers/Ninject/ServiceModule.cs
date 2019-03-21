using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Business.Abstract;
using CorporateProject.Core.Utilities.Common;
using Ninject.Modules;

namespace CorporateProject.Business.DependecyResolvers.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            //http://localhost:53066/{0}.svc  -->http://localhost:53066/ProductService.svc dönüşüyor gibi düşünebilirsin.Yani aslında bu sayfa çağırıldığında ProductManagerın yaptığı işlemleri yapıyor.Yani listeleme veya ekleme gibi işlemleri WCF servisi olarak kullanıyoruz.
            Bind<IProductService>().ToConstant(WcfProxy<IProductService>.CreateChannel());
        }
    }
}

/*
 * MVCWEBUI projesinin Global.asax'ına aşağıdakini ekle.
 *
 *    ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new ServiceModule()));
 */

/*
 MVCWEBUI projesinin webconfigine aşağıdakini ekliyoruz

 Channel factory nin çalışması için

    <!--ekliyoruz-->
    <add key="ServiceAddress" value="http://localhost:53066/{0}.svc" />
 */
