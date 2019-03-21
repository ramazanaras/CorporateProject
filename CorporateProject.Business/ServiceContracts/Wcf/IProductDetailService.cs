using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Entities.Concrete;

namespace CorporateProject.Business.ServiceContracts.Wcf
{
    [ServiceContract]
   public  interface IProductDetailService
   {
       [OperationContract]
       List<Product> GetAll();
   }
}
