using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.DataAccess.EntityFramework;
using CorporateProject.DataAccess.Abstact;
using CorporateProject.Entities.ComplexTypes;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<UserRoleItem> GetUserRoles(User user)
        {
            using (NorthwindContext context = new NorthwindContext())
            {



                //1.yöntem
                //userın rollerini getir.Çoka çok tablodan veri getiriyoruz.UserRole tablosu çoka çok bir tablodur.
                //var result = from ur in context.UserRoles
                //             join r in context.Roles
                //                 on ur.UserId equals user.Id
                //             where ur.RoleId == r.Id
                //             select new UserRoleItem { RoleName = r.Name };

                //return result.ToList();


                //2.yöntem
                //çoka çok tablodan veri getirme
                var result = context.UserRoles.Where(x => x.UserId == user.Id).Select(c => new UserRoleItem { RoleName = context.Roles.FirstOrDefault(p => p.Id == c.RoleId).Name }).ToList();
                return result;
            }
        }
    }
}
