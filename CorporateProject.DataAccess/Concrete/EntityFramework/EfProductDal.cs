using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.DataAccess.EntityFramework;
using CorporateProject.Entities.ComplexTypes;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.DataAccess.Concrete.EntityFramework
{
    public  class EfProductDal:EfEntityRepositoryBase<Product,NorthwindContext>,IProductDal
    {
        //Product operasyonları otomatik olarak yapmış olduk EfEntityRepositoryBase classı sayesinde


        //ayriyeten böyle complex tipi(ProductDetaili) getirtebiliriz
        public List<ProductDetail> GetProductDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                var result = from p in context.Products
                    join c in context.Categories on p.CategoryId equals c.CategoryId
                    select new ProductDetail
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        CategoryName = c.CategoryName
                    };
                return result.ToList();
            }
            
        }
    }
}
