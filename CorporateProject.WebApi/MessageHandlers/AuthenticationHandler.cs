using System;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CorporateProject.Business.Abstract;
using CorporateProject.Business.DependecyResolvers.Ninject;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.WebApi.MessageHandlers
{
    //apiye gelen her istek ilk önce burada karşılanır.
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                //Not:https://www.base64encode.org/ sitesinde ramazan:123 'ü encode edip Headers'da Authorization key'ine encode edilmiş değeri ver.
                //gelen requestin headerında Authorization varsa onu oku
                var token = request.Headers.GetValues("Authorization").FirstOrDefault();
                if (token != null)
                {
                    byte[] data = Convert.FromBase64String(token);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokenValues = decodedString.Split(':');


                    //AuthenticationHandler sınıfında constructor injection yapmak biraz sıkıntılı çünkü controller falan değil.Bunun  yerine böyle bir yöntem kullandık.Yani yine bağımlılığı ortadan kaldırdık.
                    IUserService userService = InstanceFactory.GetInstance<IUserService>();//burası bize UserManager sınıfını veya bir WCf servis sınıfını döndürür.BusinessModule sınıfında   Bind<IUserService>().To<UserManager>(); bu yapıyı belirliyoruz.yani herhangi bir bağımlılık yok.


                    //veritabanında kullanıcı adı ve şifre var mı
                    User user = userService.GetByUserNameAndPassword(tokenValues[0], tokenValues[1]);
                    if (user != null)
                    {
                        //kullanıcıyı authenticate ediyoruz 
                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(tokenValues[0]),
                            userService.GetUserRoles(user).Select(u => u.RoleName).ToArray());

                        //Backendde kullabilmek için (Business katmanında kullanabiliriz)
                        Thread.CurrentPrincipal = principal; //backend içinse böyle bir atama yapıyoruz.Yani Backendeki identityi set ediyoruz .Örneğin masaüstü yazılım yada mobile uyglama gibi sistemlerde authentication işlemlerde kullanabilmek için.
                       
                        //Mvc arayüzünde kullanabilmek için
                        //webdeki identityi set ediyoruz(örneğin webapi authorize işlemleri için kullanabiliriz.)
                        HttpContext.Current.User = principal; //web uygulamalarında(MVC,ASP.NET GİBİ) kullanabilmek için böyle bir atama yapıyoruz.


                      
                    }
                }
            }
            catch
            {

            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}

/*
 WebapiConfig dosyasına aşağıdaki ayarı yap

         //yapılan her isteğin önünde bu metot çalışacak.yani webapiye gelen her istek öncesi bu sınıfa düşeceğiz.
            config.MessageHandlers.Add(new AuthenticationHandler());
 */
