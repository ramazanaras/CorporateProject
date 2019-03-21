using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;

namespace CorporateProject.Core.Aspects.Postsharp.PerformanceAspects
{
    [Serializable]
    public class PerformanceCounterAspect : OnMethodBoundaryAspect
    {
        private int _interval;
        [NonSerialized]
        private Stopwatch _stopwatch;

        public PerformanceCounterAspect(int interval = 5) //bir methodun çalışma süresi 5 snden fazla ise
        {
            _interval = interval;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            _stopwatch = Activator.CreateInstance<Stopwatch>();//new'liyoruz.
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            //kronometreyi aç
            _stopwatch.Start();
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            //kronometreyi durdur.
            _stopwatch.Stop();
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                //kendimize mailde atabiliriz.Şu method şu kadar saniyede çalıştı gibi.
                Debug.WriteLine("Performance: {0}.{1}->>{2}", args.Method.DeclaringType.FullName, args.Method.Name, _stopwatch.Elapsed.TotalSeconds);
            }
            _stopwatch.Reset();
            base.OnExit(args);
        }
    }
}
