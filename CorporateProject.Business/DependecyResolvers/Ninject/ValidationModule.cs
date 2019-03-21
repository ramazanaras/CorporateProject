using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Business.ValidationRules.FluentValidation;
using CorporateProject.Entities.Concrete;
using FluentValidation;
using Ninject.Modules;

namespace CorporateProject.Business.DependecyResolvers.Ninject
{
   public  class ValidationModule:NinjectModule
    {
        public override void Load()
        {
            //client side validation
            Bind<IValidator<Product>>().To<ProductValidator>().InSingletonScope();
        }
    }
}
