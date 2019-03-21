using System;


//referanslara ekle
using System.Web.Mvc;
using System.Web.Routing;

//manage nugettan ninjecti yükle.Aslında resharper bunu bizim için referanslara ekledi.Burda bütün projelerdeki ninjectin versiyonlarının aynı olmasına dikkat etmek lazım
using Ninject;
using Ninject.Modules;


namespace CorporateProject.Core.Utilities.Infrastructure.Mvc
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;

        public NinjectControllerFactory(INinjectModule module)
        {
            _kernel = new StandardKernel(module);
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}


/*
 *MVCWEbuı'da Global.asax dosyasına ayarlama yapıyoruz
 *
 * 
            //Dependecy injection yapılandırması
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));
 *
 *
 */
