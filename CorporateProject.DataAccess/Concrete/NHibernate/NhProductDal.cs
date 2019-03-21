using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.DataAccess.Nhibernate;
using CorporateProject.Entities.ComplexTypes;
using CorporateProject.Entities.Concrete;
using NHibernate.Linq;

namespace CorporateProject.DataAccess.Concrete.NHibernate
{
    public class NhProductDal:NHEntityRepositoryBase<Product>,IProductDal
    {
        //Home controllerda bu sınıfın kullanımı
        /*
         *
         *  NhProductDal _nHproductDal=new NhProductDal(new SqlServerHelper());
         *
         *
         */

        private NHibernateHelper _nHibernateHelper;
        public NhProductDal(NHibernateHelper nHibernateHelper) : base(nHibernateHelper) //injection
        {
            _nHibernateHelper = nHibernateHelper;
        }

        //ayriyeten böyle complex tipi(ProductDetaili) getirtebiliriz
        public List<ProductDetail> GetProductDetails()
        {
            using (var session= _nHibernateHelper.OpeSession())
            {
                var result = from p in session.Query<Product>()
                    join c in session.Query<Category>() on p.CategoryId equals c.CategoryId
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
