using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CorporateProject.Core.Utilities.Mappings
{
    //KENDİ YAZDIĞIMIZ AUTOMAPPER SINIFI

    //Entityleri DTO'ya dönüştürmek için extension metot yazdık.Kendimiz Automapper yaptık aslında
    public static class TypeExtensions
    {
        //var products = _productDal.GetList().Select(x=>x.MapTo<Product>()).ToList(); bu şekilde kullandık
        public static T MapTo<T>(this object source)  //tüm object nesnelerinde bu metod çıkar. object a=5; a.MapTo çıkar.Yani aslında projedeki tüm nesnelerde bu metod çıkar.
        {
            //Reflection ile map edicez.

            //sınıfın tipi almak için
            Type hedeftip = typeof(T);//Urunler  //tipi aldık.bu sınıf şöyle bir sınıftır.şöyle propertileri vardır gibi bilgilerine erişebilcez.

            //nesnenin tipini almak için.
            Type kaynaktip = source.GetType();//UrunDTO 


            //T sonuc=new T();//bu şekilde new 'lenemez 

            //T -->UrunDTO gibi düşün. bunu aşağıda   ho.SetValue(sonuc, veri); bu şekilde değer atayabilmek için öncelikle new'lememiz(instance) lazım
            T sonuc = Activator.CreateInstance<T>();// T tipiden instance üret dedik.(CreateInstance metodu tipi belli olmayan tiplerden instance almayı sağlar.)

            PropertyInfo[] hedefozellikler = hedeftip.GetProperties();//UrunlerDTO nun propertilerine erişiyoruz gibi düşün
            PropertyInfo[] kaynakozellikler = kaynaktip.GetProperties();//Urunler nun propertilerine erişiyoruz gibi  düşün.



            foreach (PropertyInfo ko in kaynakozellikler)
            {
                PropertyInfo ho = hedefozellikler.FirstOrDefault(x => x.Name.ToLower() == ko.Name.ToLower());//UrunAdi propertisini getir gibi

                if (ho != null)
                {
                    object veri = ko.GetValue(source);//dolu source nesnesinden bizim ko propertisinin değerini bul getir dedik.örneğin UrunAdi propertisini dolu olan source'dan değeri getir dedik.

                    ho.SetValue(sonuc, veri);//sonuc nesnesine(UrunlerDTO) ho propertisine(UrunAdi) değeri yaz dedik. 
                }
            }


            return sonuc;//dolu UrunlerDTO'yu döndürdük






            return default(T);
        }


    }
}
