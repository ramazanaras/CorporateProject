using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateProject.Business.Abstract;
using CorporateProject.Business.DependecyResolvers.Ninject;
using CorporateProject.Entities.Concrete;

//IProductService in amacı arayüzü istediğimiz zaman Wcfservisi ve  istediğimiz zaman Business classlar(Örneğin ProductManager gibi) kullanmamıza olanak sağlar.DEpendency injection yöntemi ile yapıcaz bunu
//IProductService ile soyutlama yapıcaz.Yani arayüzü ister WCfServisi ile istersekte Business classlar ile besleyebileceğiz
public class ProductService:IProductService
{
    public ProductService()
    {

    }

    //parametreli constructor injection yapmaya gerek kalmadan aşağıdaki şekilde dependency injection yapabiliyoruz
    //burası bize UserManager sınıfını veya bir WCf servis sınıfını döndürür.BusinessModule sınıfında Bind<IUserService>().To<UserManager>(); bu yapıyı belirliyoruz.yani herhangi bir bağımlılık yok.
    private IProductService _productService = InstanceFactory.GetInstance<IProductService>();
    public Product Add(Product product)
    {
       return  _productService.Add(product);
    }

    public List<Product> GetAll()
    {
        return _productService.GetAll();
    }

    public Product GetById(int id)
    {
        return _productService.GetById(id);
    }

    public void TransactionalOperation(Product product1, Product product2)
    {
      _productService.TransactionalOperation(product1,product2);
    }

    public Product Update(Product product)
    {
        return _productService.Update(product);
    }
}

//Servis katmanında sadece wcf kodu yazılır.Servis katmanında Business katmanındaki kodlar yazılmaz

/*
 *webconfige connection string ekle
 *
 * <!--connectionstring ekle-->
<connectionStrings>
<add name="Northwind_Name" connectionString="data source=.;initial catalog=NORTHWND;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
 *
 */

/*
 *
 *WCFService katmanına manage nugettan entity frameworkü yükle .
 *
 */


/*
 MVCWEBUI projesinin webconfigine aşağıdakini ekliyoruz

 Channel factory nin çalışması için

    <!--ekliyoruz-->
    <add key="ServiceAddress" value="http://localhost:53066/{0}.svc" />
 */
