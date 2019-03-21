using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.CrossCuttingConcerns.Logging;
using CorporateProject.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace CorporateProject.Core.Aspects.Postsharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)] //sadece methodlara uygula.Constructorlara uygulama
    public class LogAspect : OnMethodBoundaryAspect
    {
        private Type _loggerType;
        private LoggerService _loggerService;

        //DataBaseloggger veya FileLogger gelebilir aspectde. ( [LogAspect(typeof(DatabaseLogger))])
        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType != typeof(LoggerService)) //[LogAspect(typeof(DatabaseLogger))] DatabaseLogger veya Filelogger dışında birşey yollanmışsa hata fırlat yani kız :)
            {
                throw new Exception("Wrong logger type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType); //instance al.yani new'le
            base.RuntimeInitialize(method);
        }

        //methoda girdiğimizde çalışır burası
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!_loggerService.IsInfoEnabled)//şu kişi şu saatte bu methodu çağırdı gibi bilgi logları tutucağımız için kontrol yapıyoruz
            {
                return;
            }

            try
            {
                //logladığımız methodun bilgilerini alıyoruz
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)
                }).ToList();

                //sınıf ismi namespace ismi  method ismi  gibi bilgileri alıyoruz.
                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };

                _loggerService.Info(logDetail);//logu info olarak kaydediyoruz
            }
            catch (Exception)
            {

            }

        }
    }
}
