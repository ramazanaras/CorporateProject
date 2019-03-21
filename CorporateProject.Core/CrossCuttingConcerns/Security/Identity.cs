using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CorporateProject.Core.CrossCuttingConcerns.Security
{
    //Custom Identity yapısı kuruyoruz
    public class Identity : IIdentity
    {

        //IIdentity den gelir
        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }


        //bizim yazdıklarımızı
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
