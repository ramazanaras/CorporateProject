using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;//referanslara ekle
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NHibernate.Type;

namespace CorporateProject.Core.CrossCuttingConcerns.Caching.Microsoft
{
  public  class MemoryCacheManager:ICacheManager
    {
        protected ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }
        //cache bulma
        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        //cache ekleme
        public void Add(string key, object data, int cacheTime)
        {
            if (data == null)
                return;
            //kural
            var policy=new CacheItemPolicy {AbsoluteExpiration = DateTime.Now+TimeSpan.FromMinutes(cacheTime)};
            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsAdd(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
             Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
           var keysToRemove = Cache.Where(d => regex.IsMatch(d.Key)).Select(d=>d.Key).ToList();

           foreach (var key in keysToRemove)
           {
               Remove(key);
           }


        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
    }
}
