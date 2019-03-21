using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CorporateProject.Business.DependecyResolvers.Ninject;
using CorporateProject.Core.CrossCuttingConcerns.Security.Web;
using CorporateProject.Core.Utilities.Infrastructure.Mvc;

namespace CorporateProject.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Dependecy injection yapılandırması
            //Business sınıfları üzerinden çalış
           //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));

           //Dependecy injection yapılandırması
            //WCF servisi üzerinden çalış
              ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new ServiceModule()));
        }



        //
        public override void Init()
        {
            //herhangi bir sayfaya istek(request geldiğinde) çalışır
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }


        ////herhangi bir sayfaya istek(request geldiğinde) çalışır
        //kişinin authantikasyon bilgilerine erişebildiği zamana karşılık gelir
        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                //AuthenticationHelper classında cookieye değer atamıştık.bunu burda kullanıyoruz.--->   HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;//şifreli veri
                if (String.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                //şifreli veriyi çözüyoruz
                var ticket = FormsAuthentication.Decrypt(encTicket);

                //şifreli ticket verisini önce çözüyoruz daha sonra SecurityUtilities helper classı yardımıyla Identity classına atama(doldurma) işlemi yapıyoruz 
                var securityUtlities = new SecurityUtilities();
                var identity = securityUtlities.FormsAuthTicketToIdentity(ticket);
                var principal = new GenericPrincipal(identity, identity.Roles);


                //System.Threading.Thread.CurrentPrincipal.IsInRole(roles[i])
                //SecuredOperationAspect classında yukarıdaki gibi bir kullanım yapmıştık.Bunu aşağıda dolduruyoruz 

                //Mvc arayüzünde kullanabilmek için
                HttpContext.Current.User = principal; //web uygulamalarında(MVC,ASP.NET GİBİ) kullanabilmek için böyle bir atama yapıyoruz.


                //Backendde kullabilmek için (Business katmanında kullanabiliriz)
                Thread.CurrentPrincipal = principal; //backend içinse böyle bir atama yapıyoruz.Örneğin masaüstü yazılım yada mobile uyglama gibi sistemlerde authentication işlemlerde kullanabilmek için.
            }
            catch (Exception)
            {

            }


        }




    }
}
