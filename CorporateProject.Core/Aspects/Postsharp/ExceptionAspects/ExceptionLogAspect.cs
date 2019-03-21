using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;

namespace CorporateProject.Core.Aspects.Postsharp.ExceptionAspects
{


    //hata olduğunda log tutacağımız aspect


    [Serializable]
    public class ExceptionLogAspect : OnExceptionAspect
    {
        //Herhangi bir methodda hata olduğunda buraya düşecek ve log4net aracılığıyla loglama yapacağız.

        [NonSerialized]
        private LoggerService _loggerService;
        private readonly Type _loggerType;

        //DataBaseloggger veya FileLogger gelebilir aspectde. ( [LogAspect(typeof(DatabaseLogger))])
        public ExceptionLogAspect(Type loggerType = null)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType != null)
            {
                if (_loggerType.BaseType != typeof(LoggerService))
                    throw new Exception("Wrong Logger Type");//[LogAspect(typeof(DatabaseLogger))] DatabaseLogger veya Filelogger dışında birşey yollanmışsa hata fırlat yani kız :)

                _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);//instance al.yani new'le
            }

            base.RuntimeInitialize(method);
        }


        //methodda hata olduğunda  çalışır burası
        public override void OnException(MethodExecutionArgs args)
        {
            if (_loggerService != null)
            {
                //log tipini error olarak loglama yapacağız.
                _loggerService.Error(args.Exception);
            }
        }
    }
}
