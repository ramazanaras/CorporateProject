using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CorporateProject.Business.Abstract;
using CorporateProject.Business.ValidationRules.FluentValidation;
using CorporateProject.Core.Aspects.Postsharp;
using CorporateProject.Core.Aspects.Postsharp.AuthorizationAspects;
using CorporateProject.Core.Aspects.Postsharp.CacheAspects;
using CorporateProject.Core.Aspects.Postsharp.ExceptionAspects;
using CorporateProject.Core.Aspects.Postsharp.LogAspects;
using CorporateProject.Core.Aspects.Postsharp.PerformanceAspects;
using CorporateProject.Core.Aspects.Postsharp.TransactionAspects;
using CorporateProject.Core.Aspects.Postsharp.ValidationAspects;
using CorporateProject.Core.CrossCuttingConcerns.Caching.Microsoft;
using CorporateProject.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CorporateProject.Core.CrossCuttingConcerns.Validation.FluentValidation;
using CorporateProject.Core.Utilities.Mappings;
using CorporateProject.DataAccess;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.Business.Concrete.Managers
{

    //[LogAspect(typeof(FileLogger))] classın tamamını loglar
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private readonly IQueryable<Product> _queryable;

        //DEpendecy injection ile EfProductDal veya NHibernateDal 'ı verebiliriz.Bağımlılığı ortadan kaldırdık.
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;

        }
        //   [PerformanceCounterAspect(2)] //bu metodun çalışması 2 snden fazla sürüyorsa bizi uyarsın
        [CacheAspect(typeof(MemoryCacheManager), 120)] //2 saat boyunca cachede tut
        [LogAspect(typeof(DatabaseLogger))] //bu metodu Database 'e logla diyoruz.(Filelogger dersek dosyaya loglar)
        //MVCWEBUI 'da log4net.config dosyası oluşturuyoruz ve veritabanı ve dosya yolu ayarlarını yapıyoruz.birde webconfige  <add key="log4net.Config" value="log4net.config" /> gibi ayarları ekliyoruz
        [LogAspect(typeof(FileLogger))]//dosyaya logla
     // [SecuredOperationAspect(Roles = "Admin,Editor")] //rolü sadece admin veya editor olan kullanıcılar girsin.
        public List<Product> GetAll()
        {
            //böyle bir kullanım yapmıyoruz.Böyle yazarsak Entity frameworke bağımlı oluruz.Nhibernate gibi teknolojileri kullanamayız ö yüzden yukardaki gibi interface(IProductDal) üzerinden gitmeliyiz.
            //EfProductDal efProductDal = new EfProductDal();

            //  Thread.Sleep(3000); //3 saniye beklet

            //return _productDal.GetList();



            //Kendi mapper sınıfımızla map işlemi yaptık.Burdaki amaç nesneyi serileştirilebilir bir hale getirmek.Çünkü webapide yada Wcf de servis olarak dışarıya sunmak için 
            var products = _productDal.GetList().Select(x=>x.MapTo<Product>()).ToList();
            return products;


        }

        public Product GetById(int id)
        {
            //kendi yazdığımız custom  automapperi kullandık
            return _productDal.Get(p => p.ProductId == id).MapTo<Product>();
        }
        //
        //core ve business projesine manage nugetta  postSharpı ekle 
        //PostSharp ile attribute bazında validation işlemi gerçekleştirebiliriz.
        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        public Product Add(Product product)
        {
            //Single Responsibilitye uymuyor.(SOLID'İN S 'Sİ ).Böyle bir yöntem yerine Aspect Oriented Programming(AOP) tekniğini uygulayacağız.Metot üstüne attribute mantığını yazacağız. (FluentValidate(typeof(ProductValdiator))) -->AOP Tekniği
            //böyle yapmak yerine PostSharp ile attribute bazında yaparak daha temiz bir kod uygulayabiliriz.AOP'nin temellerinden biri.öncelikle Core projesine ve Business projesine manage nugettan PostSharp yükle.
            // ValidatorTool.FluentValidate(new ProductValidator(), product); //Product classını validate ediyoruz

            //yukarda FluentValidationAspect attribute vasıtasıyla daha temiz bir kod uygulamış oluyoruz PostSharp ile
            return _productDal.Add(product);
        }

        public Product Update(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product); //Product classını validate ediyoruz



            return _productDal.Update(product);
        }


        [TransactionScopeAspect] //transaction aspecti ekledik.İşlemleri Transaction içinde gerçekleştiriyoruz.
        [FluentValidationAspect(typeof(ProductValidator))] //validation işlemleri için 
        [ExceptionLogAspect(typeof(DatabaseLogger))] //hata olduğunda log tutacağımız aspect
        [ExceptionLogAspect(typeof(FileLogger))] //hata olduğunda log tutacağımız aspect
        public void TransactionalOperation(Product product1, Product product2)
        {
            //business code
            _productDal.Add(product1);
            _productDal.Update(product2);
        }
    }
}


/*
 Arayüzde validation işlemleri genelde yapılmaz.Business katmanında yapılır.
 */
