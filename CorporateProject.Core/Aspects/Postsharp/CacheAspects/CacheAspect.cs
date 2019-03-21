using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;

namespace CorporateProject.Core.Aspects.Postsharp.CacheAspects
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private Type _cacheType;
        private int _cacheByMinute;//cache ne kadar süre kalcak
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheByMinute = 60)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);//instance aldık.new'ledik

            base.RuntimeInitialize(method);
        }

        //methodun içine girmeden 
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            //methodun bilgilerini reflection ile alıyoruz.
            var methodName = string.Format("{0}.{1}.{2}",
                args.Method.ReflectedType.Namespace,
                args.Method.ReflectedType.Name,
                args.Method.Name);
            var arguments = args.Arguments.ToList();

            var key = string.Format("{0}({1})", methodName,
                string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));
        
           
            if (_cacheManager.IsAdd(key))//cachede veri var mı diye bakıyor.
            {
                //cachede veri varsa cacheden veriyi getirir
                args.ReturnValue = _cacheManager.Get<object>(key);
            }
            base.OnInvoke(args);
            _cacheManager.Add(key, args.ReturnValue, _cacheByMinute);//cachede veri yoksa cache 'e ekler.

        }
    }
}
