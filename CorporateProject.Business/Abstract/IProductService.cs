using System.Collections.Generic;
using CorporateProject.Entities.Concrete;

//referanslara ekle
using System.ServiceModel;

namespace CorporateProject.Business.Abstract
{
    //managera bağımlı etmemek için böyle yapıyoruz.Yani bağımlılığı ortadan kaldırıyoruz.İlerde bunu WCF servisi olarak kullanabileceğiz.

   [ServiceContract]   //Wcf servisi olarak dışarıya sunmak için
    public interface IProductService
    {
        //Wcf servisi olarak dışarıya sunmak için
        [OperationContract]
        List<Product> GetAll();
        [OperationContract]
        Product GetById(int id);
        [OperationContract]
        Product Add(Product product);
        [OperationContract]
        Product Update(Product product);
        [OperationContract]
        void TransactionalOperation(Product product1,Product product2);
    }
}
