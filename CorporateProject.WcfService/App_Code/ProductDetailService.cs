using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateProject.Business.Abstract;
using CorporateProject.Business.DependecyResolvers.Ninject;
using CorporateProject.Business.ServiceContracts.Wcf;
using CorporateProject.Entities.Concrete;

/// <summary>
/// Summary description for ProductDetailService
/// </summary>
public class ProductDetailService:IProductDetailService
{
    public ProductDetailService()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private IProductService _productService = InstanceFactory.GetInstance<IProductService>();
    public List<Product> GetAll()
    {
        return _productService.GetAll();
    }
}