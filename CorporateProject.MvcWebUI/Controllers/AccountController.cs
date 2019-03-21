using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorporateProject.Business.Abstract;
using CorporateProject.Core.CrossCuttingConcerns.Security.Web;

namespace CorporateProject.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {

        //dependecy injection -->BusinessModule sınıfında ayarlamayı yapıyoruz
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        //http://localhost:64231/account/login?username=ahmet&password=123
        public string Login(string userName, string password)
        {
            var user = _userService.GetByUserNameAndPassword(userName, password);
            if (user != null)
            {
                AuthenticationHelper.CreateAuthCookie(
                    new Guid(), user.UserName,
                    user.Email,
                    DateTime.Now.AddDays(15),
                    _userService.GetUserRoles(user).Select(u => u.RoleName).ToArray(),
                    false,
                    user.FirstName,
                    user.LastName);
                return "User is authenticated!";
            }
            return "User is NOT authenticated!";
        }
    }
}