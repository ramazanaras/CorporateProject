using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorporateProject.Business.Abstract;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        //Dependecy injection yapılandırması Ninject ile bunu yaptık.BusinessModule classında işlemleri yaptık
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View(_productService.GetAll());
        }

        public string Add()
        {
            _productService.Add(new Product
            {
                CategoryId = 1,
                ProductName = "Gsm",
                QuantityPerUnit = "1",
                UnitPrice = 35
            });
            return "Added";
        }

        public string AddUpdate()
        {
            //ikinci ürünü eklerken hata vericek .bundan dolayı transaction çalışacak vede ilk ürünüde eklemeyecek.
            _productService.TransactionalOperation(new Product
            {
                CategoryId = 1,
                ProductName = "Gsm",
                QuantityPerUnit = "1",
                UnitPrice = 35
            },
                new Product
                {
                    CategoryId = 1,
                    ProductName = "Gsm",
                    QuantityPerUnit = "1",
                    UnitPrice = 10
                });
            return "Done";
        }
    }
}