using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

//core projesineden manage nugettan fluent validationı yükle
namespace CorporateProject.Core.CrossCuttingConcerns.Validation.FluentValidation
{
   public  class ValidatorTool
    {
        //ENTİTY İ VALİDATE EDİYORUZ
        public static void FluentValidate(IValidator validator, object entity)
        {
            var result = validator.Validate(entity);
            if (result.Errors.Count>0)
            {
                throw new ValidationException(result.Errors);
                
            }
        }


    }
}


/*NOT:
Validation,Cache,Authorization,Transaction yönetimi,Hata Yönetimi,Performans yönetimi gibi süreçler cross cutting concern(uygulamayı dikine keser) olarak adlandırılır.Yani projenin belli yerlerinde bunu uygulamaya ek olarak çağırırız
Cross Cutting concerni genellikle iş katmanında kullanılır
 *  */