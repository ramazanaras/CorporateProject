using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.DataAccess;
using CorporateProject.Entities.ComplexTypes;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.DataAccess
{
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductDetail> GetProductDetails();
    }
}
