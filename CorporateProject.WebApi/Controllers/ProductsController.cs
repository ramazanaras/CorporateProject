using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CorporateProject.Business.Abstract;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.WebApi.Controllers
{

    public class ProductsController : ApiController
    {

        //dependecy injection yapılandırması için NinjectWebCommon classına ayarlamalar yapıyoruz
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> Get()
        {
            return _productService.GetAll();
        }
    }
}



/*
 HATA VE ÇÖZÜMÜ

    An error occurred when trying to create a controller of type 'ProductsController'. Make sure that the controller has a parameterless public constructor.

    ÇÖZÜMÜ
    Dependecy injection ayarlarını yap.

    WebApi katmanına nugettan WebApiContrib.IoC.Ninject paketini kur.
    WebApi katmanına nugettan Ninject.MVC5 paketini kur.Bu paketi yükleyince App_Start altına NinjectWebCommon classını  yükledi.

    Birde tüm projelerdeki Ninject versiyonunun aynı olmasına dikkat et.
    
 */


/* HATA VE ÇÖZÜMÜ
 No Entity Framework provider found for the ADO.NET provider with invariant name 'System.Data.SqlClient'. Make sure the provider is registered in the 'entityFramework' section of the application config file. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
 
     
     WebApi projesine manage nugettan entity framework yükle
     */


/*HATA VE ÇÖZÜMÜ
 The underlying provider failed on Open.

    WebApi projesindeki web.confige connectionstring ekle
 */



/*HATA VE ÇÖZÜMÜ
The 'ObjectContent`1' type failed to serialize the response body for content type 'application/xml; charset=utf-8'.

  Serilize edilemeyen bir nesne döndürüyorsun.DTO olarak veriyi döndürmen gerekiyor.Normal entity sınıfları serileştirilemez.Çünkü içinde serileştirilemeyen sınıflarda var.(Örneğin context.Product.Tolist() diyoruz ama içinde Entity.Data sınıfı gibi sınıflarda var veya propertylerdeki virtual anahtar kelimesinden dolayı serileştirme olmuyor).Bu yüzden DTO olarak yani saf bir şekilde sınıfı servis etmemiz lazım.
  ******* WebApide Döngüsel Referans Yönetimi   konusuna bak.
 */
