using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.DataAccess.EntityFramework;
using CorporateProject.DataAccess.Abstact;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal:EfEntityRepositoryBase<Category,NorthwindContext>,ICategoryDal
    {
    }
}
