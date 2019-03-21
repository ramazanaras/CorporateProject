using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//referanslara ekle
using System.ServiceModel;
using System.Configuration;


namespace CorporateProject.Core.Utilities.Common
{
    //Channel Factory Implementasyonu
    public class WcfProxy<T>
    {
        //http://localhost.4535/{0}.svc   -->http://localhost.4535/ProductService.svc
        //T --> IProductService
        public static T CreateChannel()
        {
            string baseAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            string address = string.Format(baseAddress, typeof(T).Name.Substring(1));

            var binding = new BasicHttpBinding();
            var channel = new ChannelFactory<T>(binding, address);

            return channel.CreateChannel();
        }
    }
}
/*
 MVCWEBUI projesinin webconfigine aşağıdakini ekliyoruz

    <!--ekliyoruz-->
    <add key="ServiceAddress" value="http://localhost:53066/{0}.svc" />
 */
