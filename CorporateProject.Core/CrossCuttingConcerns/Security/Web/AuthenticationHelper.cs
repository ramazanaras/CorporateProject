using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace CorporateProject.Core.CrossCuttingConcerns.Security.Web
{
    public class AuthenticationHelper
    {

        public static void CreateAuthCookie(Guid id, string userName, string email, DateTime expiration, string[] roles,
            bool rememberMe, string firstName, string lastName)
        {
            //FormsAuthentication 'da bir ticket oluşturuluyor ve bu ticket şifreli olarak tutuluyor
            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, rememberMe,
                CreateAuthTags(email, roles, firstName, lastName, id));

            string encTicket = FormsAuthentication.Encrypt(authTicket);//ticketi şifreliyoruz

            //şifrelenmiş ticketimızı cookieye ekliyoruz.MVCWEbUı'da Global.asaxda bu bilgiyi kullanıyoruz
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));


            /*
             Global.asax'da
               var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
               bu şekilde cookiyi alıp kullanıyoruz.
             */
        }


        //kendi istediğimiz datayı oluşrutuyoruz ve bunuda FormsAuthentication'a yukarda veriyoruz
        private static string CreateAuthTags(string email, string[] roles, string firstName, string lastName, Guid id)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(email);
            stringBuilder.Append("|");
            for (int i = 0; i < roles.Length; i++)
            {
                stringBuilder.Append(roles[i]);
                if (i < roles.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Append("|");
            stringBuilder.Append(firstName);
            stringBuilder.Append("|");
            stringBuilder.Append(lastName);
            stringBuilder.Append("|");
            stringBuilder.Append(id);
            return stringBuilder.ToString();
        }
    }
}
